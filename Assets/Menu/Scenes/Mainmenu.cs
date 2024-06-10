using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Scene 1");
        Debug.Log("Play Scene 1 Succeed");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting ...");
    }
}