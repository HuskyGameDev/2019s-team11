using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    public Transform bar;
    public float maxHP = PlayerStats.maxHP;
    public float currentHP = PlayerStats.playerHealth;

     void Update()
    {
        bar.transform.localScale = new Vector3(normalize(currentHP, maxHP), 1, 0);
    }
    private void Start()
    {
        currentHP = maxHP;
      
    }

    public float normalize(float curHP, float maxHP)
    {
        return curHP / maxHP;
    }

}