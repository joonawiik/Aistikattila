using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
