using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int damage;
    public bool player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTrigger(Collider2D collision)
    {
        if (player)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyStatsScript>().Health -= damage;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                //collision.gameObject.GetComponent<PlayerHealthScript>().Health -= damage;
            }
        }
    }
}
