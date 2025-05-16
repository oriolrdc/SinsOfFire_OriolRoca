using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private float _fireBallSpeed = 10;
    private float _fireBallDamage = 5;
    private Animator _animator;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidBody.AddForce(transform.right * _fireBallSpeed, ForceMode2D.Impulse);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            Enemy enemyScript = collider.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(_fireBallDamage);
            FireBallDeath();
        }

        if(collider.gameObject.layer == 3)
        {
            FireBallDeath();
        }
    }

    void FireBallDeath()
    {
        _animator.SetBool("Death", true);
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        Destroy(gameObject, 0.5f);
    }
}
