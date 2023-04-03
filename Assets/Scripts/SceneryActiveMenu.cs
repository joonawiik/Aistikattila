using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryActiveMenu : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        //SceneManager.LoadScene("Interface");
        //Display.displays[0].Activate();
        Application.Quit();
        Debug.Log("Returned to main menu interface.");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            ReturnToMainMenu();
    }
}
