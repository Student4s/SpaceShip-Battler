using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableModule : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float armor;

    public void GetDamage(float damage)
    {
        hp -= Math.Clamp(damage-armor,1,damage);
        if(hp<=0)
            Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }
}