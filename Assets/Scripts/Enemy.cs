using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //MOVIMIENTO
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    [SerializeField] private int _driection = 1;
    [SerializeField] private float _enemySpeed = 2;
    [SerializeField] private int _actualDirection = 1;

    //VIDA
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth = 5;

    //CUCHILLO
    [SerializeField] private PolygonCollider2D _triggerCuchillo;
    [SerializeField] private PlayerContol _playerControl;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _triggerCuchillo = GetComponent<PolygonCollider2D>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_enemySpeed * _driection, _rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3 && _driection == -1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(collision.gameObject.layer == 3 && _driection == 1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _playerControl.Death();
        }
    }

    void OnBecameVisible()
    {
        _driection = _actualDirection;
    }

    void OnBecameInvisible()
    {
        _actualDirection = _driection;
        _driection = 0;
    }

    public void Death()
    {
        _driection = 0;
        _rigidBody.gravityScale = 0;
        _boxCollider.enabled = false;
        Destroy(gameObject, 2f);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            Death();
        }
    }
}
