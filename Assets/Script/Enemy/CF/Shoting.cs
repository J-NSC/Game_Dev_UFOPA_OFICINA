using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform BulletSpwan;
    
    public float teste = 3;
    
    Enemy enemy;
    float time;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }


    public void Shoot()
    {
        Instantiate(bulletPrefab, BulletSpwan.position, Quaternion.identity);
    }

}
