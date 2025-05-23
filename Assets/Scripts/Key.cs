using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    private SoundManager _soundManager;
    private PlayerContol _playerControl;
    private BoxCollider2D _boxCollider;
    public GameObject winCanvas;
    public AudioSource _audioSource;
    public AudioClip _winSFX;
    public float _delay = 5;

    void Awake()
    {
        _soundManager = GameObject.Find("BGM Manager").GetComponent<SoundManager>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        winCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _soundManager.Win();
            StartCoroutine(WinRoutine());
            _playerControl.PlayerWin();
            StartCoroutine(_soundManager.WinCavas());
        }
    }

    public IEnumerator WinRoutine()
    {
        _audioSource.PlayOneShot(_winSFX);
        yield return new WaitForSeconds(_delay);
        winCanvas.SetActive(true);

    }
}
