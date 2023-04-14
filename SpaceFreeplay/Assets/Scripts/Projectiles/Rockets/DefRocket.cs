using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DefRocket : Missile
{
    private void Start()
    {
       // target = GameObject.FindWithTag("Point").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(0,0,speed*Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
}
