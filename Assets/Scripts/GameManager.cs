using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isPlaying = true;
    public bool isPaused = false;
    private SoundManager _soundManager;
    [SerializeField] private GameObject _pauseCanvas;
    public int _diamonds = 0;
    public Text diamondsText;
    public int _Kills = 0;
    public Text KillsText;

    void Awake()
    {
        _soundManager = GameObject.Find("BGM Manager").GetComponent<SoundManager>();
    }

    void Start()
    {
        _pauseCanvas.SetActive(false);
        diamondsText.text = "0" + _diamonds.ToString();
        KillsText.text = "0" + _Kills.ToString();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    public void AddDiamonds()
    {
        _diamonds++;
        diamondsText.text = "0" + _diamonds.ToString();
    }

    public void Kills()
    {
        _Kills++;
        KillsText.text = "0" + _Kills.ToString();
    }

    public void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            _soundManager.PauseBGM();
            _pauseCanvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            _soundManager.PauseBGM();
            _pauseCanvas.SetActive(true);
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
        _soundManager.PauseBGM();
        _pauseCanvas.SetActive(false);
    }
}
