using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public enum MenuState { Main, Manager, New };
    public MenuState currentMenu;

    public GameObject mainMenu;
    public GameObject managerMenu; 
    public GameObject newMenu; 

    void Awake()
    {
        currentMenu = MenuState.Main;
    }

    void Update()
    {
    	switch (currentMenu)
	{
	    case MenuState.Main:
		mainMenu.SetActive(true);
		managerMenu.SetActive(false);
		newMenu.SetActive(false);
    		break;

	    case MenuState.Manager:
		mainMenu.SetActive(false);
		managerMenu.SetActive(true);
		newMenu.SetActive(false);
    		break;

	    case MenuState.New:
		mainMenu.SetActive(false);
		managerMenu.SetActive(false);
		newMenu.SetActive(true);
    		break;
	}
    } 

    // When opening scenery manager menu
    public void OpenManagerMenu()
    {
	currentMenu = MenuState.Manager;
        Debug.Log("Scenery Manager opened.");
    }

    // When opening new scenery menu
    public void OpenNewSceneryMenu()
    {
	currentMenu = MenuState.New;
        Debug.Log("Menu for adding new sceneries opened.");
    }


    // Play, or as we call it, launch
    public void LaunchForest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Debug.Log("Forest Scene Loaded.");
    }

    public void LaunchMountain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
        Debug.Log("Mountain Scene loaded.");
    }

    public void LaunchTurku()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +0);
        Debug.Log("Turku Scene unfortunately not implemented yet.");
    }

    public void LaunchSea()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +0);
        Debug.Log("Sea Scene unfortunately not implemented yet.");
    }
    // Quit
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting happened.");
    }
}
