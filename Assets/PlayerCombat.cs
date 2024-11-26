using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private Vector2 facingDirection = Vector2.down; // Default to facing down
    private Vector2 movement;
    public int attackDamage = 25; // Damage dealt per attack
    public float attackRange = 1.5f; // Range of the attack
    public float attackAngle = 60f; // Angle of the attack cone (in degrees)
    public LayerMask enemyLayer; // Layer of enemies

    void Update()
    {
        // Get movement input
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Update facing direction if there is movement
        if (movement.magnitude > 0)
        {
            facingDirection = movement.normalized; // Update facing direction
        }

        // Update animator parameters for movement
        animator.SetFloat("Horizontal", facingDirection.x);
        animator.SetFloat("Vertical", facingDirection.y);

        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
        if (Input.GetButtonDown("Attack"))
    {
        Debug.Log("Attack button pressed!");
    }
    }

    void Attack()
    {
        // Ensure correct direction parameters are passed for the attack animation
        animator.SetFloat("Horizontal", facingDirection.x);
        animator.SetFloat("Vertical", facingDirection.y);
        animator.SetTrigger("Attack");
        
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Filter enemies based on facing direction
        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float angleToEnemy = Vector2.Angle(facingDirection, directionToEnemy);

            if (angleToEnemy <= attackAngle / 2) // Check if within the attack cone
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log($"Hit {enemy.name}!");
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Vector3 leftLimit = Quaternion.Euler(0, 0, -attackAngle / 2) * facingDirection;
        Vector3 rightLimit = Quaternion.Euler(0, 0, attackAngle / 2) * facingDirection;

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftLimit * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightLimit * attackRange);
    }
}