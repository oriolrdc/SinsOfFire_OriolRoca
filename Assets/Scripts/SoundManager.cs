using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip _BGM;
    [SerializeField] AudioClip _GameOver;

    void awake()
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
}
