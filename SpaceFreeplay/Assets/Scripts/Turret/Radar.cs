using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : ShipModule
{
    public List<GameObject> targets;

    public Transform currentTarget;

    public void TargetUpdate()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
                targets.Remove(targets[i]);
            else
            {
                currentTarget= targets[i].GetComponent<Transform>();
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            targets.Add(other.gameObject);
            TargetUpdate();
        }
    }

    
}
