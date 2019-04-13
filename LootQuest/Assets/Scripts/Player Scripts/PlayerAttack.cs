using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startingTimeBetweenAttack;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;


    public GameObject meleeHitbox;
    public float meleeDamage;
    public float rangedDamage;


    // Start is called before the first frame update
    void Start()
    {
        meleeHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetButtonDown("MeleeAttack"))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyStats>().TakeDamage();
                }
                timeBetweenAttack = startingTimeBetweenAttack;
            } 
        } else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("MeleeAttack"))
        {
            meleeAttack();
        }
        if (Input.GetButtonDown("RangedAttack"))
        {
            rangedAttack();
        }
    }

    private void meleeAttack()
    {
        //play animation
        //spawn hitbox
        meleeHitbox.SetActive(true);
        //deal damage if something is in hitbox
        
        //despawn hitbox
        meleeHitbox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            damageEnemy(meleeDamage);
        }
    }

    private void rangedAttack()
    {
        //play animation
        //spawn projectile
    }

    private void damageEnemy(float damage)
    {

    }
}
