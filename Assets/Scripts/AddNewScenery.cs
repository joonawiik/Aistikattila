using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SFB;

public class AddNewScenery : MonoBehaviour
{
    public AssetBundle bundle;
    private string[] paths;

    // Called upon clicking 'Choose Scenery File', opens file browser popup
    public void OpenFileBrowser()
    {
        paths = StandaloneFileBrowser.OpenFilePanel("Choose Asset Bundle", "", "", false);
        Debug.Log(paths[0]);
        loadSceneFromFile(paths[0]);
    }

    // Takes the chosen file path and tries loading all scene paths within the bundle
    // NB! should catch if chosen file not bundle?
    public void loadSceneFromFile(string bundlePath)
    {
        bundle = AssetBundle.LoadFromFile(bundlePath);

        string[] scenePaths = bundle.GetAllScenePaths();
        Debug.Log(scenePaths[0]);

        string sceneName = Path.GetFileNameWithoutExtension(scenePaths[0]);
        SceneManager.LoadScene(sceneName);
    }
}
