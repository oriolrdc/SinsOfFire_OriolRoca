using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private float _fireBallSpeed = 10;
    private float _fireBallDamage = 2;
    //private Animator _animator;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_animator = GetComponent<Animator>();
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
            //_animator.SetBool("Death");
            FireBallDeath();
        }

        if(collider.gameObject.layer == 3)
        {
            //_animator.SetBool("Death");
            FireBallDeath();
        }
    }

    void FireBallDeath()
    {
        //_spriteRenderer.enabled = false;
        //_rigidBody.gravityScale = 0;
        Destroy(gameObject);
    }
}
