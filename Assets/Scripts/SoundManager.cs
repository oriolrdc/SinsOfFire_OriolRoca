using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _BGM;
    [SerializeField] private AudioClip _GameOver;

    void Awake()
    {
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
    }
}
