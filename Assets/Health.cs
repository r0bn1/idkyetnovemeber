using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    public int currentHealth; // Current health

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal (optional)
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Cap at maxHealth
        Debug.Log($"{gameObject.name} healed by {amount}. Current Health: {currentHealth}");
    }

    // Handle death
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Replace with custom death behavior if needed
    }
}
