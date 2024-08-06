using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HealthModificationType
{
    Healer,
    Damage
}

public class TakeDamage : MonoBehaviour
{
    
    public void ModifyHealth(float value, HealthModificationType healthModificationType, float currentHealth, HealthBar healthBar, float maxHealth)
    {
        if(healthModificationType == HealthModificationType.Healer)
        {
            if (currentHealth <= maxHealth)
            {
                currentHealth += value;
            }
        }
        else
        {
            currentHealth -= value;
        }
        
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
