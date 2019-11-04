using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        AudioManager.audioManager.MenuSound();
        SceneManager.LoadScene("Main Menu");   
    }

    public void Return()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
  
    }

    public void InstructionsPage()
    {
        SceneManager.LoadScene("Instructions Page");
    }
}
