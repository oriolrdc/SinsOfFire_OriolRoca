using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContol : MonoBehaviour
{
    //Movimiento
    [Header("Movimiento")]
    [SerializeField] private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidBody;
    public float inputHorizontal;
    public float playerSpeed = 4.5f;
    //Salto
    private GroundSensor _groundSensor;
    public float jumpForce = 10;
    
    //Dash
    [Header("Dash")]
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
    //SFX
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioClip _gameOverSFX;
    //Sonidos
    [Header("Sonidos")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _footStepsSFX;
    [SerializeField] private AudioClip _dashSFX;
    [SerializeField] private AudioClip _jumpSFX;
    private bool _alreadyPlaying = false;
    //ATAQUE
    [Header("Ataque")]
    [SerializeField] private float _attackDamage = 2.5f;
    [SerializeField] private float _attackRadius = 1;
    [SerializeField] private Transform _hitBoxPosition;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private AudioClip _slash;
    [SerializeField] private float _attackColdown = 1;
    //AMERICAN
    [Header("Disparo")]
    [SerializeField] private Transform _fireBallSpawn;
    [SerializeField] private GameObject _fireBallPrefab;
    public AudioClip _fireBallSFX;
    private bool _hasMana = true;
    [SerializeField] private float _fireballCost = 0.35f;
    //VIDA
    [Header("Vida")]
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] private Image _healthBar;
    [SerializeField] private AudioClip _damage;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Header("ConstantDamage")]
    [SerializeField] private float _damageColdown = 1;
    [Header("Mana")]
    [SerializeField] private float _currentMana;
    [SerializeField] private float _maxMana = 1;
    [SerializeField] private Image _manaBar;
    //MANAGERS
    [Header("Managers")]
    [SerializeField] private SoundManager _soundManager;

    public Cofres _chests;
    public bool _IsChestHere;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleTransform = _particleSystem.transform;
        _particlePosition = _particleTransform.localPosition;
        _healthBar = GameObject.Find("VidaBarra").GetComponent<Image>();
        _manaBar = GameObject.Find("ManaBarra").GetComponent<Image>();
        _soundManager = GameObject.Find("BGM Manager").GetComponent<SoundManager>();
        _chests = GameObject.Find("cofre").GetComponent<Cofres>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _currentMana = _maxMana;
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _maxHealth;
        _manaBar.fillAmount = _maxMana;

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

        if (Input.GetButtonDown("Jump"))
        {
            if(_groundSensor.isGrounded || _groundSensor.canDoubleJump)
            {
                _SFXSource.PlayOneShot(_jumpSFX);
                Jump();
            }
        }

        Movement();

        if(Input.GetButtonDown("Fire1") && _hasMana)
        {
            FireBall(_fireballCost);
        }

        if(Input.GetButtonDown("Fire2"))
        {
            NormalAtack();
        }

        FootStepsSound();

        if(Input.GetButtonDown("Submit") && _IsChestHere)
        {
            _chests.OpenChest();
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            _IsChestHere = true;
        }

        if(collider.gameObject.layer == 7)
        {
            Death();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        _IsChestHere = false;
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
        if(!_groundSensor.isGrounded)
        {
            _groundSensor.canDoubleJump = false;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            //animacionesDobleSalto
        }
        _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _animator.SetBool("IsJumping", true);
        //audio
    }

    IEnumerator Dash()
    {
        _SFXSource.PlayOneShot(_dashSFX);
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
            //particulas
            _particleTransform.SetParent(gameObject.transform);
            _particleSystem.Play();
            _particleTransform.localPosition = _particlePosition;
            _particleTransform.rotation = transform.rotation;
            //audio
            _audioSource.clip = _footStepsSFX;
            _audioSource.loop = true;
            _audioSource.Play();
            //cosas
            _alreadyPlaying = true;
        }
        else if(!_groundSensor.isGrounded || Input.GetAxisRaw("Horizontal") == 0)
        {
            //particulas
            _particleTransform.SetParent(null);
            _particleSystem.Stop();
            //audio
            _audioSource.loop = false;
            _audioSource.Stop();
            //cosas
            _alreadyPlaying = false;
        }
    }
    
    void NormalAtack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_hitBoxPosition.position, _attackRadius, _enemyLayer);
        _SFXSource.PlayOneShot(_slash);
        _animator.SetTrigger("IsAttacking");
        
        foreach(Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage(_attackDamage);
        }
    }

    void FireBall(float cost)
    {
        Instantiate(_fireBallPrefab, _fireBallSpawn.position, _fireBallSpawn.rotation);
        _animator.SetTrigger("Shooting");
        _SFXSource.PlayOneShot(_fireBallSFX);

        _currentMana -= cost;
        _manaBar.fillAmount = _currentMana;

        if(_currentMana <= 0)
        {
            _hasMana = false;
        }
        else if(_currentMana > 0)
        {
            _hasMana = true;
        }
    }   

    public void TakeDamage(float damage)
    {
        _healthBar.fillAmount = _currentHealth -= damage;
        _SFXSource.PlayOneShot(_damage);

        if(_currentHealth <= 0)
        {
            Death();
        }
    }

    public bool canstantDamage = false;

    public IEnumerator ConstantDamage(float damage)
    {
        yield return new WaitForSeconds(_damageColdown);

        while(canstantDamage)
        {
            _currentHealth -= damage;
            _healthBar.fillAmount = _currentHealth;
            _SFXSource.PlayOneShot(_damage);
            
            if(_currentHealth <= 0)
            {
                Death();
            }

            yield return new WaitForSeconds(_damageColdown);
        }
    }

    public void Death()
    {
        _spriteRenderer.enabled = false;
        _rigidBody.gravityScale = 0;
        _boxCollider.enabled = false;
        Destroy(_groundSensor.gameObject);
        inputHorizontal = 0;
        _audioSource.Stop();
        _SFXSource.PlayOneShot(_gameOverSFX);
        _soundManager.GameOver();
        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitBoxPosition.position, _attackRadius);
    }

    public void RestoreMana()
    {
        _currentMana += 0.35f;
        _manaBar.fillAmount = _currentMana;
        _hasMana = true;
    }

    public void RestoreHealth()
    {
        _currentHealth += 0.5f;
        _healthBar.fillAmount = _currentHealth;
    }
}
