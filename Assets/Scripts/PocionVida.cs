using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocionVida : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polyCollider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerContol _playerControl;
    [SerializeField] private AudioSource _audioSource;
    public AudioClip pocionVidaSFX;

    void Awake()
    {
        
        _polyCollider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _playerControl.RestoreHealth();
            _audioSource.PlayOneShot(pocionVidaSFX);
            Death();
        }
    }

    void Death()
    {
        _spriteRenderer.enabled = false;
        _polyCollider.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
