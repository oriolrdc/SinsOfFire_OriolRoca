using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;
    public bool canDoubleJump = true;

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
            canDoubleJump = true;
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
