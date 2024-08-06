using System;
using UnityEngine;

public enum PlayerState
{
    Idle    = 0,
    Walker  = 1,
    Attack  = 2,
    Jump    = 3,
    Stagger = 4,
    Death   = 5,
    Squat   = 6,
    Fall    = 7
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public float speed;
    public float forceJump = 3f;

    [SerializeField] float maxHealth = 10f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject feetPosition;
    [SerializeField] LayerMask groundLayer;
    
    public Rigidbody2D rb; 
    SpriteRenderer playerSprite;
    Animator anim;

    float currentHealth;
    float direction;
    bool isGrounded;

    public static Player Instance { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    void Update()
    {
      
        HandleInput();
        UpdatePlayerState();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.transform.position, 0.3f, groundLayer);
        if(playerState != PlayerState.Death)
            MovePlayer();
    }

    void HandleInput()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerState = PlayerState.Squat;
        }
    }

    void MovePlayer()
    {
        if (direction != 0 && playerState != PlayerState.Jump)
        {
            playerState = PlayerState.Walker;
        }
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Jump()
    {
        playerState = PlayerState.Jump;
        anim.SetTrigger("Jumper");
        rb.velocity = Vector2.up * forceJump;
    }

    void UpdatePlayerState()
    {
        if (currentHealth <= 0 && playerState != PlayerState.Death)
        {
            playerState = PlayerState.Death;
            anim.SetTrigger("Death");
        }

        if (rb.velocity.magnitude < 0.2f && playerState != PlayerState.Death)
        {
            playerState = PlayerState.Idle;
        }

        if (rb.velocity.y < -1)
        {
            playerState = PlayerState.Fall;
        }

        FlipPlayer();
    }

    void FlipPlayer()
    {
        if (direction != 0)
        {
            playerSprite.flipX = direction < 0;
        }
    }

    void UpdateAnimation()
    {
        anim.SetInteger("State", (int)playerState);
    }

    public void ModifyHealth(float value, HealthModificationType healthModificationType)
    {
        if (healthModificationType == HealthModificationType.Healer)
        {
            currentHealth = Mathf.Min(currentHealth + value, maxHealth);
        }
        else
        {
            currentHealth -= value;
        }
        
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}