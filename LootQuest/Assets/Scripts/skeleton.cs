using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : EnemyStatsScript
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // need to add ai for skeleton
    }
}
