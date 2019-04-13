using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyStats
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = transform.position;
    }

    void CheckDistance()
    {
        if(Vector2.Distance(target.position, transform.position) <= chaseRange && Vector3.Distance(target.position, transform.position) >= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(target.position, transform.position) >= chaseRange * 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, spawnPoint, moveSpeed * Time.deltaTime);
        }
    }



    // Update is called once per frame
    void Update()
    {
        CheckDistance();

    }


}
