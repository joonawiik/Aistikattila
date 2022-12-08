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
        sceneDictionary.Add(sceneDictionary.Count + 1, "ForestScenery");
        sceneDictionary.Add(sceneDictionary.Count + 1, "MountainScenery");
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
        sceneDictionary.Add(id, name);
    }

    public string getName(int id)
    {
        return sceneDictionary[id];
    }
}
