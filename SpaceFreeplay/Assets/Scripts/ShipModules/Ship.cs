using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private ShipModule[] modules;
    [SerializeField] private Reactor reactor;
    void Start()
    {
        for (int i = 0; i < modules.Length; i++)
            reactor.minHeat += modules[i].minEnergyConsume;
    }

    public void EmergencyOff()
    {
        for (int i = 0; i < modules.Length; i++)
        {
            modules[i].gameObject.SetActive(false);
        }
    }

    public void RechargeModules()
    {
        for (int i = 0; i < modules.Length; i++)
        {
            modules[i].gameObject.SetActive(true);
        }
    }
}
