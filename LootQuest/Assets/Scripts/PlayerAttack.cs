using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
        Attack();
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
