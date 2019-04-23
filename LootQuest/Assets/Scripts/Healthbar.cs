using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    public GameObject bar;
    public float maxHP = PlayerStats.maxHP;
    public float currentHP = PlayerStats.playerHealth;


    private void Awake()
    {
        bar = GameObject.Find("BarSprite");
       
    }
    void Update()
    {
        currentHP = PlayerStats.playerHealth;
        maxHP = PlayerStats.maxHP;
        bar.transform.localScale = new Vector3(normalize(currentHP, maxHP), 1f, 0f);
    }
    private void Start()
    {
        currentHP = PlayerStats.playerHealth;
        maxHP = PlayerStats.maxHP;
      
    }

    public float normalize(float curHP, float maxHP)
    {
        return (curHP / maxHP);
    }

}