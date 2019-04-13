using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    private void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButtonDown("MeleeAttack"))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyStats>().TakeDamage(damage);
                }
                timeBetweenAttack = startTimeBetweenAttack;
            }
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
