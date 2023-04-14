using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModule : MonoBehaviour
{
    [SerializeField] public float minEnergyConsume;
    [SerializeField] protected float energyConsumePerUse;
    

    public float UseModule()
    {
        return energyConsumePerUse;
    }
}
