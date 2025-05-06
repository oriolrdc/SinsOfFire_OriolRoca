using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    private PolygonCollider2D _polygonCollider;
    private AudioSource _audioSource;
    public AudioClip diamondSFX;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(diamondSFX);
            _spriteRenderer.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
