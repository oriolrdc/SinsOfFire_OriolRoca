using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _BGM;
    [SerializeField] private AudioClip _GameOver;
    [SerializeField] private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayBGM();
    }

    void PlayBGM()
    {
        _audioSource.clip = _BGM;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void Win()
    {
        _audioSource.Pause();
    }

    public void GameOver()
    {
        _audioSource.Pause();
        SceneManager.LoadScene(2);
    }

    public void PauseBGM()
    {
        if(_gameManager.isPaused)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.Play();
        }
    }
}
