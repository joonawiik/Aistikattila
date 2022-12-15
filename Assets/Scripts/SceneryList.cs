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
    public GameObject emptyText;
    public List<GameObject> clones;

    void Start()
    {
        updateSceneryList();
    }

    // called upon making changes to the dictionary, i.e. saving a new scenery or deleting one
    public void updateSceneryList()
    {

        // deletes all previously instantiated clones
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }

        Debug.Log("started, dictionary count: " + dictionary.getCount());
        Dictionary<int, string> copy = dictionary.getDic();        

        for (int i = 0; i < dictionary.getCount(); i++)
        {
            Vector3 vector;

            // list for scenery manager menu
            if (emptyText == null)
            {
                vector = new Vector3(cloneObject.transform.position.x, -(i * 30f) + 120, 0);
            }
            // list for library menu that doesn't show the pre-made sceneries & has a text when empty
            else
            {
                if (dictionary.getCount() == 2)
                {
                    emptyText.SetActive(true);
                    break;
                }
                else if (i == 0 || i == 1)
                {
                    continue;
                }
                vector = new Vector3(cloneObject.transform.position.x, cloneObject.transform.position.y - (i - 2) * 15f, 0);
                emptyText.SetActive(false);
            }

            // instantiates a separate clone object for each scenery in dictionary
            GameObject clone =
                Instantiate(cloneObject, vector,
                cloneObject.transform.rotation,
                cloneObject.transform.parent);
            clone.transform.parent = transform;
            clone.SetActive(true);
            clone.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = dictionary.getName(i + 1);
            clones.Add(clone);
        }
    }

    public void deleteScenery(int id)
    {
        dictionary.deleteItem(id);
    }
}
