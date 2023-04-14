using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketV2GS : Missile
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
    }
    
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
}
