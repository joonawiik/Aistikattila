"""
Created by Team Aistikattila within the scope of the Capstone
project @ 2022

The script can be used to interface with the Helvar router which
controls the lights in the Aistikattila room of Flavoria, UTU.
This could also be implemented fully in Python using the requests
lib, so feel free to fix.

The IP address of the Helvar router is for now statically assigned
to 192.168.0.50. If this were to change, fix the old address in the
beginning of this script.
"""
import json
import netrc

import requests as req

from bs4 import BeautifulSoup

helvar_ip = "192.168.0.50"
helvar_hostname = "helvar_router"
login_url = "/login.aspx"
scenes_url = "/Pages/Control/Scenes.aspx"

headers = {
    'Host': helvar_ip,
    'Upgrade-Insecure-Requests': '1',
    'Origin': f'http://{helvar_ip}',
    'Content-Type': 'application/x-www-form-urlencoded',
    'User-Agent': 'Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.13) '
                  'Gecko/20101203 Firefox/3.6.13 ( .NET CLR 3.5.30729)',
    'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9',
    'Accept-Language': 'en-GB,en-US;q=0.9,en;q=0.8',
    'Connection': 'close',
}

with req.Session() as s:
    res = s.get(
        f"http://{helvar_ip}{login_url}",
        headers=headers,
        verify=False,
    )

    # extract the hidden __VIEW value
    soup = BeautifulSoup(res.text, 'html.parser')
    view_var = str(soup.find(id="__VIEWSTATE")).split("\"")[-2]

    # auth_tokens structure is the following, as there is no account
    # password stored.
    #
    # [username, None, password]
    auth_tokens = netrc.netrc().authenticators(helvar_hostname)
    data = {
        '__VIEWSTATE': view_var,
        'userName': 'anonymous',
        'Login1$UserName': auth_tokens[0],
        'Login1$Password': auth_tokens[2],
        'Login1$LoginSubmitBtn': 'Login',
    }

    # Adapt the headers
    headers["Referer"] = f'http://{helvar_ip}{login_url}'
    s.post(
        f'http://{helvar_ip}{login_url}',
        headers=headers,
        data=data,
        verify=False
    )

    headers["Referer"] = f'http://{helvar_ip}{scenes_url}'
    headers["Content-Type"] = 'application/json; charset=utf-8'
    headers["Accept"] = "*/*"
    headers["X-Requested-With"] = 'XMLHttpRequest'

    group_id = 100
    params = {
        "group": str(group_id)
    }
    res = s.get(
        f'http://{helvar_ip}{scenes_url}/GetScenes',
        headers=headers,
        data=data,
        params=params,
        verify=False
    )
    lights_json_data = res.json()
    lighting_options = {}

    desired_colour_scheme = "Pinkki"

    # test out trying to turn off the lights
    i = 0
    for scene_data in lights_json_data["d"]:
        if not scene_data["Visible"]:
            continue

        lighting_options[scene_data["Name"]] = {
            "possible_names": [scene_data["Name"]],
            "id": scene_data["SceneId"]["BlockAndSceneNo"],
            "group": scene_data["SceneId"]["GroupNo"]
        }
        if scene_data["AlternativeName"]:
            lighting_options[scene_data["Name"]]["possible_names"].append(scene_data["AlternativeName"])

    print(json.dumps(lighting_options, indent=4))

    # find a match for the given desired scheme
    for key, data in lighting_options.items():
        if desired_colour_scheme not in data["possible_names"]:
            continue

        print(f"Match found for scheme {key} with ID {data['id']}")
        params = {
            "GroupNo": str(group_id),
            "SceneNum": str(data['id'])
        }
        res = s.get(
            f'http://{helvar_ip}{scenes_url}/CallScene',
            headers=headers,
            params=params,
            verify=False
        )

