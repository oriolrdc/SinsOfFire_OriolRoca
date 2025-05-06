using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;
    public bool canDoubleJump = true;
    private PlayerContol _playerControl;

    void Awake()
    {
        _playerControl = GetComponentInParent<PlayerContol>();
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
            canDoubleJump = true;
        }

        if(collider.gameObject.layer == 7)
        {
            _playerControl.Death();
        }
    }

    void OnTriggerStay2D(Collider2D collider) 
    {
        isGrounded = true; 
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        isGrounded = false; 
    }
}
