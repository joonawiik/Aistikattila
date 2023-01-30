"""
Created by Team Aistikattila within the scope of the Capstone
project @ 2022

The script can be used to interface with the Helvar router which
controls the lights in the Aistikattila room of Flavoria, UTU.

The IP address of the Helvar router is for now statically assigned
to 192.168.0.50. If this were to change, fix the old address in the
beginning of this script.
"""
import argparse
import json
import netrc

import requests as req

from bs4 import BeautifulSoup


def parse_args():
    """
    Parse arguments.
    """
    parser = argparse.ArgumentParser(
        description="A Python v3 script to interface with the Helvar lights within"
        "the Aistikattila room, UTU."
    )
    parser.add_argument(
        "scheme-name",
        type=str,
        help="The name of the lighting scheme used. The possible options can be seen in"
        "the Helvar USee UI.",
    )
    return parser.parse_args()


def main():
    args = parse_args()

    helvar_ip = "192.168.0.50"
    helvar_hostname = "helvar_router"
    login_url = "/login.aspx"
    scenes_url = "/Pages/Control/Scenes.aspx"

    headers = {
        "Host": helvar_ip,
        "Upgrade-Insecure-Requests": "1",
        "Origin": f"http://{helvar_ip}",
        "Content-Type": "application/x-www-form-urlencoded",
        "User-Agent": "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.13) "
        "Gecko/20101203 Firefox/3.6.13 ( .NET CLR 3.5.30729)",
        "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9",
        "Accept-Language": "en-GB,en-US;q=0.9,en;q=0.8",
        "Connection": "close",
    }

    with req.Session() as s:
        res = s.get(
            f"http://{helvar_ip}{login_url}",
            headers=headers,
            verify=False,
        )
        res.raise_for_status()

        # extract the hidden __VIEW value from the HTML dump
        soup = BeautifulSoup(res.text, "html.parser")
        view_var = str(soup.find(id="__VIEWSTATE")).split('"')[-2]

        # auth_tokens structure is the following, as there is no account
        # password stored.
        # [username, None, password]
        auth_tokens = netrc.netrc().authenticators(helvar_hostname)
        data = {
            "__VIEWSTATE": view_var,
            "userName": "anonymous",
            "Login1$UserName": auth_tokens[0],
            "Login1$Password": auth_tokens[2],
            "Login1$LoginSubmitBtn": "Login",
        }

        # Adapt the headers
        headers["Referer"] = f"http://{helvar_ip}{login_url}"
        res = s.post(
            f"http://{helvar_ip}{login_url}", headers=headers, data=data, verify=False
        )
        res.raise_for_status()

        # Adapt some more headers
        headers["Referer"] = f"http://{helvar_ip}{scenes_url}"
        headers["Content-Type"] = "application/json; charset=utf-8"
        headers["Accept"] = "*/*"
        headers["X-Requested-With"] = "XMLHttpRequest"

        # By default, we will be using the populated 100 group
        group_id = "100"
        params = {"group": group_id}
        res = s.get(
            f"http://{helvar_ip}{scenes_url}/GetScenes",
            headers=headers,
            data=data,
            params=params,
            verify=False,
        )
        res.raise_for_status()
        lights_json_data = res.json()
        lighting_options = {}

        # Build a dict for the present available lighting schemes. Example structure:
        #   <scene name>: {
        #       "possible_names": repeated name and other alternative names available to
        #                         ensure we can easily search for any names given
        #       "id": ID of the scene within the USee UI, needed for calling the scene
        #       "group": group of the scene needed for calling the scene
        #   }, ...
        for scene_data in lights_json_data["d"]:
            if not scene_data["Visible"]:
                continue

            lighting_options[scene_data["Name"]] = {
                "possible_names": [scene_data["Name"]],
                "id": scene_data["SceneId"]["BlockAndSceneNo"],
                "group": scene_data["SceneId"]["GroupNo"],
            }
            if scene_data["AlternativeName"]:
                lighting_options[scene_data["Name"]]["possible_names"].append(
                    scene_data["AlternativeName"]
                )

        print(json.dumps(lighting_options, indent=4))

        # find a match for the given desired scheme
        for key, data in lighting_options.items():
            if args.scheme_name not in data["possible_names"]:
                continue

            print(f"Match found for scheme {key} with ID {data['id']}")
            params = {"GroupNo": group_id, "SceneNum": str(data["id"])}
            res = s.get(
                f"http://{helvar_ip}{scenes_url}/CallScene",
                headers=headers,
                params=params,
                verify=False,
            )
            res.raise_for_status()


if __name__ == "__main__":
    main()
