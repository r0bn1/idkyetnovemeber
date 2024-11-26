using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the player
    public float attackCooldown = 1f; // Time between attacks
    private float nextAttackTime = 0f;


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
