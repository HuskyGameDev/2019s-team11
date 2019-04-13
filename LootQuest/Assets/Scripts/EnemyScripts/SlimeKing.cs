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
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            //play death animation
            //spawn loot?
            //death effect?
            Instantiate(enemy);
            Instantiate(enemy);
            Instantiate(enemy);
            Instantiate(enemy);

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
        Instantiate(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckTimer();
        if (timer > 0)
        {
            Move();
        }
        else
        {
            SpawnSlimes();
            ResetTimer();
        }
        

    }
}
