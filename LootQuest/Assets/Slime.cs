﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyStatsScript
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= ChaseRange && Vector3.Distance(target.position, transform.position) >= AttackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }
}
