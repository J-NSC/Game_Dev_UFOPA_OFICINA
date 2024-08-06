using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableSO collectableSO;

    SpriteRenderer collectableSprite;

    Player player;
    
    void Awake()
    {
        collectableSprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        collectableSprite.sprite = collectableSO.sprite; 
    }

    void Update()
    {
        
    }
    
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectableSO.collectabeltype == CollectabelType.moeda)
            {
                HudController.inst.score++;
            }
            else
            {
                // player.ModifyHealth(collectableSO.hpValue, HealthModificationType.Healer);
            }
            
            Destroy(this.gameObject);
        }
    }
}
