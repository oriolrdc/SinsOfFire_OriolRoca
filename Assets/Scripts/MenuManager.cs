using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject controlesCanvas;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ControlesEnter()
    {
        controlesCanvas.SetActive(true);
    }

    public void ControlesExit()
    {
        controlesCanvas.SetActive(false);
    }
}
