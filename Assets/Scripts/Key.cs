using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private SoundManager _soundManager;
    private PlayerContol _playerControl;
    private BoxCollider2D _boxCollider;

    void Awake()
    {
        _soundManager = GameObject.Find("BGM Manager").GetComponent<SoundManager>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _soundManager.Win();
        }
    }
}
