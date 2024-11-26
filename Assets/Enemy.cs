using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the player
    public float attackCooldown = 1f; // Time between attacks
    private float nextAttackTime = 0f;
    
    public GameObject healthBarPrefab; // Assign the prefab in the Inspector
    private GameObject healthBarInstance;
    private Image healthBarForeground;

    public Transform healthBarPosition; // Position to anchor the health bar above the enemy
    private Health enemyHealth; // Reference to the enemy's health script

    void Start()
    {
        // Get enemy health script
        enemyHealth = GetComponent<Health>();

        // Create health bar instance
        healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);

        // Attach the health bar to the enemy
        healthBarInstance.transform.SetParent(GameObject.Find("Canvas").transform, false); // Parent to UI Canvas
        healthBarForeground = healthBarInstance.transform.Find("HealthBarBackground/HealthBarForeground").GetComponent<Image>();
    }

    void Update()
    {
        // Update health bar position to stay above the enemy
        if (healthBarPosition != null)
        {
            healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(healthBarPosition.position);
        }
        else
        {
            healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        }

        // Update health bar fill amount
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (enemyHealth != null && healthBarForeground != null)
        {
            float healthPercent = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
            healthBarForeground.fillAmount = healthPercent;
        }
    }

    void OnDestroy()
    {
        // Destroy the health bar when the enemy is destroyed
        Destroy(healthBarInstance);
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            // Deal damage to the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
