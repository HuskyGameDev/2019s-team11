using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void onTriggerEnter(Collider other)
    {
        System.Random rand = new System.Random();

        if (other.gameObject.CompareTag("Player"))
        {

            PlayerStats.gold += rand.Next(10,101);
            
            this.gameObject.SetActive(false);
        }
    }
}
