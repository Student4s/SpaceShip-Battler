using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketV1 : Missile
{
    [SerializeField] private float sideSpeed = 1f;

    private void Start()
    {
        target = GameObject.FindWithTag("Point").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(sideSpeed*Time.deltaTime,0,speed*Time.deltaTime);
       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
}
