using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SFB;
using TMPro;

public class AddNewScenery : MonoBehaviour
{

    public AssetBundle bundle;
    private string[] bundlePaths;

    private string[] scenePaths;
    public string sceneName;

    public GameObject nameInput;
    public GameObject descriptionInput;

    public DictionaryScript sceneDictionary;

    // Called upon clicking 'Choose Scenery File', opens file browser popup
    public void OpenFileBrowser()
    {
        bundlePaths = StandaloneFileBrowser.OpenFilePanel("Choose Asset Bundle", "", "", false);
        Debug.Log(bundlePaths[0]);
        LoadSceneFromFile(bundlePaths[0]);
    }

    // Takes the chosen file path and tries loading all scene paths within the bundle
    // NB! should catch if chosen file not bundle?
    public void LoadSceneFromFile(string bundlePath)
    {
        bundle = AssetBundle.LoadFromFile(bundlePath);

        scenePaths = bundle.GetAllScenePaths();
        Debug.Log(scenePaths[0]);
        sceneName = Path.GetFileNameWithoutExtension(scenePaths[0]);
        Debug.Log(sceneName);
        
        //SceneManager.LoadScene(sceneName);
    }

    // Called upon writing something in the Description field.
    // The only point is to calculate and show the length of the contents in the Description field.
    public void Calculate()
    {
        //Debug.Log("Count:" + GameObject.Find("Description InputField").GetComponent<TMP_InputField>().text);
        //Debug.Log(GameObject.Find("Description InputField").GetComponent<TMP_InputField>().text.Length);
        //Debug.Log(GameObject.Find("Add New Scenery Description").GetComponent<TMP_Text>().text);
        GameObject.Find("Add New Scenery Description").GetComponent<TMP_Text>().text = "Description (" +GameObject.Find("Description InputField").GetComponent<TMP_InputField>().text.Length +"/150)";
    }


    // Called upon clicking 'Save'
    public void SaveNewScene()
    {

        sceneDictionary.addItem(nameInput.GetComponent<TMP_InputField>().text);
        //FileUtil.CopyFileOrDirectory(scenePaths[0], "Assets/Scenes/" + sceneName + ".unity");
        Debug.Log("saved, dictionary count: " + sceneDictionary.getCount());
    }
}
