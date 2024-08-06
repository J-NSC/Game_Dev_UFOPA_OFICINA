using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    jump,
}

public class Enemy : MonoBehaviour
{
    EnemyState enemyState;
    
    // public delegate void ShotingHunterHandler(float Dir);
    // public static event ShotingHunterHandler shotingHunter;
    [SerializeField] EnemySO enemySo;
    [SerializeField] GameObject target;
    
    [SerializeField] Animator anim;
    [SerializeField] GameObject posIni;

    [SerializeField]  float forceJump = 3f;
    [SerializeField] bool hasJump = false;

    bool facingRight = true;
    Rigidbody2D rbEnemy;
    float heath;

    void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        heath = enemySo.hp;
    }

    void Start()
    {
        enemyState = EnemyState.idle;
    }

    void Update()
    {
        float tempDist = Vector3.Distance(target.transform.position, transform.position);
        UpdateAnimation();

        if (tempDist <= enemySo.visionRange)
        {
            enemyState = EnemyState.walk;

            Vector3 direction = (target.transform.position - transform.position).normalized;
            rbEnemy.velocity = new Vector2(direction.x * enemySo.speed, rbEnemy.velocity.y);

            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            // hasJump = true;
            Attack(tempDist);
        }
        else
        {
            ReturnToInitialPosition();
        }

        if (heath <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    
    void ReturnToInitialPosition()
    {
        float distanceToInitial = Vector3.Distance(posIni.transform.position, transform.position);

        if (distanceToInitial > 1f)
        {
            Vector3 direction = (posIni.transform.position - transform.position).normalized;
            rbEnemy.velocity = new Vector2(direction.x * enemySo.speed, rbEnemy.velocity.y);

            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            rbEnemy.velocity = Vector2.zero;
            enemyState = EnemyState.idle;
            hasJump = false;
        }
    }

    void JumpEnemy()
    {
        if (hasJump)
            rbEnemy.velocity = Vector2.up * forceJump;
    }
    
    void UpdateAnimation()
    {
        anim.SetInteger("State", (int) enemyState);
    }

    void Attack(float tempDist)
    {
        if (tempDist <= enemySo.attackArea)
        {
            enemyState = EnemyState.attack;
            rbEnemy.velocity = Vector2.zero;
            // Player.Instance.ModifyHealth(enemySo.damageValue, HealthModificationType.Damage);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        float positionAdjustment = 0.1f;
        
        if (facingRight)
        {
            transform.position += new Vector3(positionAdjustment, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(positionAdjustment, 0, 0);
        }
    }


    public void TakeDamage(int damage)
    {
        heath -= damage;
    }
    void OnDrawGizmos()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemySo.visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySo.attackArea);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, direction * enemySo.visionRange);
    }
}