using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSv2 : FCSbased
{
    [SerializeField] private Transform prevTarget;
    [SerializeField] private Vector3 deltaRotation;
    [SerializeField] private Vector3 deltaRotation2;
    [SerializeField] private float deltaTime = 0f;

    public override GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {
        if (prevTarget == null)
        {
            float rocketSpeed = target.GetComponent<Missile>().speed;
            float distance = Vector3.Distance(target.position, gunTower.position);
            float delta = distance * rocketSpeed / currentBulletSpeed;

            crutch.transform.position = target.position;
            var rotation = target.rotation;
            crutch.transform.rotation = rotation;
            crutch.transform.Translate(0, 0, delta);

            deltaTime = 0f;
            prevTarget = target;
            deltaRotation.x = rotation.x;
            deltaRotation.y = rotation.y;
            deltaRotation.z = rotation.z;
        }
        else
        {
            var rotation = target.rotation;
            
            float rocketSpeed = target.GetComponent<Missile>().speed;
            float distance = Vector3.Distance(target.position, gunTower.position);
            float delta = distance * rocketSpeed / currentBulletSpeed;
            float t2 = distance / currentBulletSpeed;
            
            deltaRotation2.x = (rotation.x - deltaRotation.x)* t2 / deltaTime;
            deltaRotation2.y = (rotation.y - deltaRotation.y)* t2 / deltaTime;
            deltaRotation2.z = (rotation.z - deltaRotation.z)* t2 / deltaTime;

            crutch.transform.position = target.position;
            crutch.transform.rotation = rotation;
            crutch.transform.Rotate(deltaRotation2);
            crutch.transform.Translate(0, 0, delta);
            
            deltaRotation.x = rotation.x;
            deltaRotation.y = rotation.y;
            deltaRotation.z = rotation.z;
            deltaTime = 0f;
        }
        return crutch;
    }

    private void Update()
    {
        deltaTime += Time.deltaTime;
    }
}