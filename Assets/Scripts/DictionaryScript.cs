using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript : MonoBehaviour
{

    // Dictionary that includes all existing scenes (with individual ids). Is called to create library view
    public Dictionary<int, string> sceneDictionary;

    void Start()
    {
        sceneDictionary = new Dictionary<int, string>();
        sceneDictionary.Add(sceneDictionary.Count + 1, "Forest Scenery");
        sceneDictionary.Add(sceneDictionary.Count + 1, "Mountain Scenery");

    }

    public int getCount()
    {
        return sceneDictionary.Count;
    }

    public Dictionary<int, string> getDic()
    {
        return sceneDictionary;
    }

    public void addItem(int id, string name)
    {
        Debug.Log("Adding scenery: " + name);
        sceneDictionary.Add(id, name);
    }

    public string getName(int id)
    {
        return sceneDictionary[id];
    }

    public void deleteItem(int id)
    {
        Debug.Log("Removing scenery: " + sceneDictionary[id]);
        sceneDictionary.Remove(id);
    }
}
