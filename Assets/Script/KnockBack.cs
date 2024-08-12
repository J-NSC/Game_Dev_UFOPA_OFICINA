using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float KnockUpForce;
    [SerializeField] float KnockBackForce;
    [SerializeField] float damage;
    [SerializeField] float knockTime;
    [SerializeField] Player player;
    bool enemyDeath;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        var enemy = other.GetComponent<Enemy>();
        
        if(player.rb.velocity.y < -1)
        {
            player.playerState = PlayerState.Jump;
            player.rb.AddForce(Vector2.up * KnockUpForce, ForceMode2D.Impulse);
            enemy.TakeDamage(1);
        }
        
        
        if(player.playerState != PlayerState.Stagger)
        {
            var knockBackDir = (player.rb.position.x > other.transform.position.x)
                ? new Vector2(0.5f, 0.5f)
                : new Vector2(-0.5f, 0.5f);
            player.playerState = PlayerState.Stagger;
            player.rb.AddForce(knockBackDir * KnockBackForce, ForceMode2D.Impulse);
            enemy.enemyState = EnemyState.idle;
            StartCoroutine(knockBackTime(knockTime));
        }
        }
    }
    
    IEnumerator knockBackTime(float time)
    {
        yield return new WaitForSeconds(time);
        player.playerState = PlayerState.Idle;
        player.rb.velocity = Vector2.zero;
    }

   
}