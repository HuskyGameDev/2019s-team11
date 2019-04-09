using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyStatsScript
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPoint = transform.position;
    }

    void CheckDistance()
    {
        if(Vector2.Distance(target.position, transform.position) <= ChaseRange && Vector3.Distance(target.position, transform.position) >= AttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(target.position, transform.position) >= ChaseRange * 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, SpawnPoint, MoveSpeed * Time.deltaTime);
        }
    }

    void CheckHealth()
    {
        if (Health <= 0)
        {
            //play death animation
            //spawn loot?
            //death effect?

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckDistance();
    }
}
