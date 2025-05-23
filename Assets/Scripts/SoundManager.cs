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
    public float delay = 5;
    public float delay2 = 2;
    public float delay3 = 3.5f;
    public float delay4 = 14;
    public AudioClip _winCanvas;
    public AudioClip _missionComplete;

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

    public IEnumerator GameOver()
    {
        _audioSource.Stop();
        yield return new WaitForSeconds(delay);
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

    public IEnumerator WinCavas()
    {
        yield return new WaitForSeconds(delay2);
        _audioSource.clip = _winCanvas;
        _audioSource.Play();
        yield return new WaitForSeconds(delay3);
        _audioSource.PlayOneShot(_missionComplete);
        yield return new WaitForSeconds(delay4);
        SceneManager.LoadScene(0);
    }
}
