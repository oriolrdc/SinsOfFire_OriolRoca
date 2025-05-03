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

    //Particulas
    private ParticleSystem _particleSystem;
    private Transform _particleTransform;
    private Vector3 _particlePosition;

    //Animaciones
    private Animator _animator;
    //Sonidos

    //FootSteps
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _FootSteps;
    private bool _alreadyPlaying = false;

    //ATAQUE
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] private float _attackRadius = 1;
    [SerializeField] private Transform _hitBoxPosition;
    [SerializeField] private LayerMask _enemyLayer;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleTransform = _particleSystem.transform;
        _particlePosition = _particleTransform.localPosition;
        
    }

    void Update()
    {
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

        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && _groundSensor.isGrounded == true)
        {
            Jump();
        }

        Movement();

        if(Input.GetButtonDown("Fire2"))
        {
            NormalAtack();
        }

        FootStepsSound();
    }

    void FixedUpdate() 
    {
        //DASH 
        if(_isDahsing)
        {
            return;
        }
        _rigidBody.velocity = new Vector2(inputHorizontal * playerSpeed, _rigidBody.velocity.y);
    }

    void Jump()
    {
        _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        _animator.SetTrigger("IsDashing");
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
    }

    void Movement()
    {
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
    }

    void FootStepsSound()
    {
        if(_groundSensor.isGrounded && Input.GetAxisRaw("Horizontal") != 0 && !_alreadyPlaying)
        {
            _particleTransform.SetParent(gameObject.transform);
            _particleTransform.localPosition = _particlePosition;
            _particleTransform.rotation = transform.rotation;
            _audioSource.Play();
            _particleSystem.Play();
            _alreadyPlaying = true;
        }
        else if(!_groundSensor.isGrounded || Input.GetAxisRaw("Horizontal") == 0)
        {
            _particleTransform.SetParent(null);
            _audioSource.Stop();
            _particleSystem.Stop();
            _alreadyPlaying = false;
        }
    }

    void NormalAtack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_hitBoxPosition.position, _attackRadius, _enemyLayer);

        foreach(Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage(_attackDamage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitBoxPosition.position, _attackRadius);
    }
}
