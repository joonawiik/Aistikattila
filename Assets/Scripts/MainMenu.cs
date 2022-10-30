using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play, or as we call it, launch
    public void Launch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    // Quit
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting happened.");
    }
}
