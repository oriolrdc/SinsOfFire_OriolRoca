using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //SPRITES
    [SerializeField] SpriteRenderer _spriteRenderer;
    //MOVIMIENTO
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    [SerializeField] private int _driection = 1;
    [SerializeField] private float _enemySpeed = 2;
    [SerializeField] private int _actualDirection = 1;
    //VIDA
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth = 5;
    [SerializeField] private Slider _healthBar;
    //CUCHILLO
    [SerializeField] private PolygonCollider2D _triggerCuchillo;
    [SerializeField] private PlayerContol _playerControl;
    [SerializeField] private float _cuchilloDamage = 0.35f;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _triggerCuchillo = GetComponent<PolygonCollider2D>();
        _playerControl = GameObject.Find("personaje").GetComponent<PlayerContol>();
        _healthBar = GetComponentInChildren<Slider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_enemySpeed * _driection, _rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3 && _driection == -1 || collision.gameObject.layer == 6 && _driection == -1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(collision.gameObject.layer == 3 && _driection == 1 || collision.gameObject.layer == 6 && _driection == 1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _playerControl.TakeDamage(_cuchilloDamage);
            _playerControl.canstantDamage = true;
            StartCoroutine(_playerControl.ConstantDamage(_cuchilloDamage));
        }

        if(collider.gameObject.layer == 8 && _driection == -1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(collider.gameObject.layer == 8 && _driection == 1)
        {
            _driection *= -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            _playerControl.canstantDamage = false;
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

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        _healthBar.value = _currentHealth;

        if(_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        _driection = 0;
        _rigidBody.gravityScale = 0;
        _boxCollider.enabled = false;
        _triggerCuchillo.enabled = false;
        _spriteRenderer.enabled = false;
        Destroy(gameObject, 2f);
    }

    
}
