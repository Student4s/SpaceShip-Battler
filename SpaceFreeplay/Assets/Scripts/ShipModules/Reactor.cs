using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    public float totalHeat;
    [SerializeField] private float maxHeat;
    [SerializeField] public float minHeat;
    [SerializeField] private float coolingPerSecond;
    private bool emergency = false;

    [SerializeField] private Ship ship;

    private void Start()
    {
        BMD.Use += AddHeat;
    }

    void Update()
    {
        totalHeat=Math.Clamp( totalHeat -= coolingPerSecond * Time.deltaTime,minHeat,maxHeat);
       
        if(totalHeat>=maxHeat)
            Overheat();

        if (totalHeat == 0 && emergency)
        {
            emergency = false;
            ship.RechargeModules();
        }
            
    }

    void Overheat()
    {
        emergency = true;
        ship.EmergencyOff();
    }

    public void AddHeat(float heat)
    {
        totalHeat += heat;
    }
}
