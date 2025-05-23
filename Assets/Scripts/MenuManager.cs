using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject controlesCanvas;
    public GameObject menuPricipalCanvas;
    public GameManager _gameManager;
    
    void Start()
    {
        try
        {
        controlesCanvas.SetActive(false);
        }
        catch
        {
            Debug.Log("NoHay");
        }
    }

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
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
