using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("RootWin");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
