using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _fireBallSpeed = 10;
    private float _fireBallDamage = 2;
    
    void Awake()
    {
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
        Destroy(gameObject);
    }
}
