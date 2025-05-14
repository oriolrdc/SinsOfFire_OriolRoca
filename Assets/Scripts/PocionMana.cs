using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocionMana : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polyCollider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerContol _playerControl;
    public float _manaInIt = 1;

    void Awake()
    {
        _polyCollider = GetComponent<PolygonCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _playerControl.RestoreMana();
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }


}
