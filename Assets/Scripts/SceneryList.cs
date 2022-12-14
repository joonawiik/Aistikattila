using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryList : MonoBehaviour
{
    public Dictionary<int, string> dic;
    public GameObject cloneObject;
    public GameObject textObject;
    public GameObject SceneryListPanel;
    public DictionaryScript dictionary;

    void Start()
    {
        updateSceneryList();
    }

    public void updateSceneryList()
    {
        Debug.Log("started, dictionary count: " + dictionary.getCount());
        Dictionary<int, string> copy = dictionary.getDic();

        for (int i = 0; i < dictionary.getCount(); i++)
        {
            GameObject clone =
                Instantiate(cloneObject,
                new Vector3(cloneObject.transform.position.x, -(i * 30f) + 120, 0),
                cloneObject.transform.rotation,
                cloneObject.transform.parent);
            clone.transform.parent = transform;
            clone.SetActive(true);
            clone.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = dictionary.getName(i + 1);
        }

        //Dictionary<int, string> dic = AddNewScenery.sceneDictionary;

        //foreach (KeyValuePair<int, string> scenery in dic)
        //{
        //    Debug.Log("jotain löyty" + scenery.Value);

        //    GameObject newButton = new GameObject();
        //    newButton.name = "ButtonName"; //Optional
        //    newButton.AddComponent<Button>();
        //button_list.Add(newButton);

    }

    public void deleteScenery(int id)
    {
        dictionary.deleteItem(id);
    }
}
