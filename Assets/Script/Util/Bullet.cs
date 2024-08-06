using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 3f ;
    Player player;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        Vector3 dir = target.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, 0).normalized * speed;

        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player.ModifyHealth(3f, HealthModificationType.Damage);
            Destroy(this.gameObject);
        }
    }
}
