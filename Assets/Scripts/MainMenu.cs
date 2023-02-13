using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public enum MenuState { Main, Manager, New };
    public MenuState currentMenu;

    public GameObject mainMenu;
    public GameObject managerMenu; 
    public GameObject newMenu;

    public GameObject forestAdjustmentBox;
    public ToggleGroup forestTimeOfDay;
    public ToggleGroup forestWeather;
    public ToggleGroup forestSound;
    public enum ForestTimeOfDayState { Day, Night };
    public ForestTimeOfDayState currentForestTimeOfDay;
    public enum ForestWeatherState { Sunny, Rainy };
    public ForestWeatherState currentForestWeather;
    public enum ForestSoundState { On, Off };
    public ForestSoundState currentForestSound;

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

    public void OpenMainMenu()
    {
        currentMenu = MenuState.Main;
        Debug.Log("Main Menu opened.");
    }

    // When opening scenery manager menu
    public void OpenManagerMenu()
    {
        //sceneryPanel.updateSceneryList();
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
        Debug.Log(currentForestTimeOfDay);
        Debug.Log(currentForestWeather);
        Debug.Log(currentForestSound);

        // If the adjustment box is active, the scene won't be launched
        if (forestAdjustmentBox.active)
        {
            Debug.Log("The forest adjustment box is active, it needs to be closed and the changed settings saved before launching the scene.");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            Debug.Log("Forest Scene Loaded.");
        }
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

    public void LaunchWinterForest()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +0);
        Debug.Log("Winter Forest Scene unfortunately not implemented yet.");
    }

    public void LaunchMovingTrain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +0);
        Debug.Log("Moving Train Scene unfortunately not implemented yet.");
    }

    // Called upon clicking the Adjust button in the Forest box
    public void AdjustForest()
    {
        Debug.Log("Adjusting Forest scenery settings.");
        forestAdjustmentBox.SetActive(true);
    }

    // Called upon clicking the Apply button in the Forest adjustment box
    public void StopAdjustingForest()
    {
        Toggle selectedTimeOfDay = forestTimeOfDay.ActiveToggles().FirstOrDefault();
        if (selectedTimeOfDay.name.ToString() == "Day")
        {
            currentForestTimeOfDay = ForestTimeOfDayState.Day;
        }
        else if (selectedTimeOfDay.name.ToString() == "Night")
        {
            currentForestTimeOfDay = ForestTimeOfDayState.Night;
        }
        Toggle selectedWeather = forestWeather.ActiveToggles().FirstOrDefault();
        if (selectedWeather.name.ToString() == "Sunny")
        {
            currentForestWeather = ForestWeatherState.Sunny;
        }
        else if (selectedWeather.name.ToString() == "Rainy")
        {
            currentForestWeather = ForestWeatherState.Rainy;
        }
        Toggle selectedSound = forestSound.ActiveToggles().FirstOrDefault();
        if (selectedSound.name.ToString() == "On")
        {
            currentForestSound = ForestSoundState.On;
        }
        else if (selectedSound.name.ToString() == "Off")
        {
            currentForestSound = ForestSoundState.Off;
        }
        Debug.Log(selectedTimeOfDay);
        forestAdjustmentBox.SetActive(false);
    }

    // Quit
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting happened.");
    }
}
