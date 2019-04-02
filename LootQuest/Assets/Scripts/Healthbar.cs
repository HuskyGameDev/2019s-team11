using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{

    public Transform bar;
    private float maxHP = 100;
    public float currentHP;

    private void Start()
    {
        currentHP = maxHP;
        takeDamage(50f);
    }

    public float normalize(float curHP, float maxHP)
    {
        return curHP / maxHP;
    }

    public void takeDamage(float dmg)
    {
        currentHP -= dmg;
        float newHealth = normalize(currentHP, maxHP);
        bar.transform.localScale = new Vector3(newHealth, 1, 0);
    }


}