using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContol : MonoBehaviour
{
    //Movimiento
    private Rigidbody2D _rigidBody;
    public float inputHorizontal;
    public float playerSpeed = 4.5f;
    
    //Salto
    private GroundSensor _groundSensor;
    public float jumpForce = 10;
    
    //Dash
    [SerializeField] private float _dashForce = 20;
    [SerializeField] private float _dashDuration = 2f;
    [SerializeField] private float _dashCoolDown = 1;
    private bool _canDash = true;
    private bool _isDahsing = false;

    //Animaciones
    private Animator _animator;
    //Sonidos

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //MOVEMENT
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(inputHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsWalking", true);
        }
        else if(inputHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        //JUMP
        if(Input.GetButtonDown("Jump") && _groundSensor.isGrounded == true)
        {
            Jump();
        }

        _animator.SetBool("IsJumping", !_groundSensor.isGrounded);

        //DASH
        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        if(_isDahsing)
        {
            return;
        }
    }

    void FixedUpdate() 
    {
        _rigidBody.velocity = new Vector2(inputHorizontal * playerSpeed, _rigidBody.velocity.y);    

        //DASH 
        if(_isDahsing)
        {
            return;
        }
    }

    void Jump()
    {
        _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        Debug.Log("Dash Iniciado");
        float gravity = _rigidBody.gravityScale;
        _rigidBody.gravityScale = 0;
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        _isDahsing = true;
        _canDash = false;
        _rigidBody.AddForce(transform.right * _dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_dashDuration);
        _rigidBody.gravityScale = gravity;
        _isDahsing = false;
        yield return new WaitForSeconds(_dashCoolDown);
        _canDash = true;
        Debug.Log("Dash Finalizado");
    }
}
