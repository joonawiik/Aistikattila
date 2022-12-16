using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript : MonoBehaviour
{

    // Dictionary that includes all existing scenes (with individual ids). Is called to create library view
    public List<string> sceneDictionary;

    void Start()
    {
        sceneDictionary = new List<string>();
        sceneDictionary.Add("Forest Scenery");
        sceneDictionary.Add("Mountain Scenery");
    }

    public int getCount()
    {
        return sceneDictionary.Count;
    }

    public List<string> getDic()
    {
        return sceneDictionary;
    }

    public void addItem(string name)
    {
        Debug.Log("Adding scenery: " + name);
        sceneDictionary.Add(name);
    }

    public string getName(int id)
    {
        return sceneDictionary[id];
    }
}
