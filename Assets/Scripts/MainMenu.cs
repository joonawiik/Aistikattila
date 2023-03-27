using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
//using UnityEditor.Scripting.Python;
using UnityEditor;
using System.IO;


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

    public SettingDataManager.TimeOfDay parameter1;
    public SettingDataManager.Weather parameter2;
    public SettingDataManager.Sound parameter3;

    // For the lighting:
    public string scriptPath;

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
        // Just an example of launching with adjustment data, you can delete this once you get it
        // Let's check what is currently chosen and then pass the parameters to the SettingDataManager
        if (currentForestTimeOfDay == ForestTimeOfDayState.Night)
        {
            parameter1 = SettingDataManager.TimeOfDay.Night;
        }
        else
        {
            parameter1 = SettingDataManager.TimeOfDay.Day;
        }
        if (currentForestWeather == ForestWeatherState.Rainy)
        {
            parameter2 = SettingDataManager.Weather.Rainy;
        }
        else
        {
            parameter2 = SettingDataManager.Weather.Sunny;
        }
        if (currentForestSound == ForestSoundState.Off)
        {
            parameter3 = SettingDataManager.Sound.Off;
        }
        else
        {
            parameter3 = SettingDataManager.Sound.On;
        }  
        SettingDataManager.PassData(
            // 1 = Time of day, 2 = Weather, 3 = Sound
            parameter1,
            parameter2,
            parameter3
        );

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        
        // For the lighting:
        //scriptPath = Path.Combine(Application.dataPath,"python_req_test.py");
        //PythonRunner.RunFile(scriptPath);
        Debug.Log("Forest Scene loaded with these parameters: "+parameter1+", "+parameter2+", "+parameter3);
    }

    public void LaunchSpace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
        Debug.Log("Space Scene loaded.");
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

    public void LauchAddedScenery(GameObject lauchableClone) 
    {
        string name = lauchableClone.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        Scene[] scenes = SceneManager.GetAllScenes();
        List<string> names = new List<string>();

        foreach (Scene sc in scenes) {
            names.Add(sc.name);
        }

        try {
            if (names.Contains(name))
            {
                SceneManager.LoadScene(name);
            }
            else
            {
                lauchableClone.transform.GetChild(2).gameObject.SetActive(true);
                Debug.Log("No assetbundle scenery added");
            }
        }
        catch {
            Debug.Log("Unexpected error loading the scenery");
        }
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

    // Called upon clicking the Quit button in the upper right corner
    public void Quit()
    {
        Debug.Log("Quitting happened.");
        Application.Quit();
    }
}
