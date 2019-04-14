using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : EnemyStats
{
    public Transform target;
    private float timer = 5f;
    public Transform[] spawnpoints;
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Move()
    {
        if (Vector2.Distance(target.position, transform.position) >= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            //play death animation
            //spawn loot?
            //death effect?
            SpawnSlimes();
            

            Destroy(gameObject);
        }
    }

    void CheckTimer()
    {
        timer = timer - Time.deltaTime;
    }
    void ResetTimer()
    {
        if (timer <= 0)
        {
            timer = 5f;
        }
    }

    void SpawnSlimes()
    {
        if (target.position.y < transform.position.y)
        {
            Instantiate(enemy, new Vector3(transform.position.x, transform.position.y - 5), Quaternion.identity);
        }
        else if (target.position.x < transform.position.x)
        {
            Instantiate(enemy, new Vector3(transform.position.x - 5, transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(enemy, new Vector3(transform.position.x + 5, transform.position.y), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckTimer();
        if (timer > 1f)
        {
            Move();
        }
        else if (timer > .01f)
        {

        }
        else
        {
            SpawnSlimes();
            ResetTimer();
        }
        
    }
}
