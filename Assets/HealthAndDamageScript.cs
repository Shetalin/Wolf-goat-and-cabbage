using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDamageScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public HealtBarScript healthBar;
    public bool isAttacked;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        isAttacked = true;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
