using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1UI : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        Debug.Log("Play Again Complete");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game Succeed");
    }
}
