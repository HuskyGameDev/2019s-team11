using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public int attackRange;
    public int chaseRange;
    public int attackPower;
    public Vector2 spawnPoint;
    public Vector2 range;
    public Transform enemyAttackPosition;
    public float attackTimerStart = 1f;
    public float attackTimer = 1f;
    public float waitForAttack = .5f;
    public bool shouldAttack;
    public LayerMask whatIsPlayer;

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage... Bat health =" + health);
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("should die");
        Destroy(gameObject);
    }

    public void MeleeAttack()
    {
        if (attackTimer <= 0)
        {
            if (waitForAttack > 0)
            {
                waitForAttack = waitForAttack - Time.deltaTime;
            }
            else
            {
                attackTimer = attackTimerStart;
                waitForAttack = .5f;
                shouldAttack = false;
                range = new Vector2(0, 0);
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(enemyAttackPosition.position, attackRange, whatIsPlayer);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    playerToDamage[i].GetComponent<PlayerStats>().takeDamage(attackPower);
                }
            }
        }
        else
        {
            attackTimer = attackTimer - Time.deltaTime;
        }
    }
}
