using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSv2 : FCSv1
{
    [SerializeField] private Transform prevTarget;
    [SerializeField] private Vector3 deltaOffset;
    [SerializeField] private Vector3 currentOffset;
    [SerializeField] private float deltaTime = 0f;

    public override GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {
        if (prevTarget == null)
        {
            rocketSpeed = target.GetComponent<Missile>().speed;
            var position = target.position;

            deltaTime = 0f;
            prevTarget = target;
            deltaOffset.x = position.x;
            deltaOffset.y = position.y;
            deltaOffset.z = position.z;
        }
        else
        {
            var position = target.position;
            float distance = Vector3.Distance(position, gunTower.position);
            
            currentOffset.x = (position.x - deltaOffset.x)*distance/(deltaTime*currentBulletSpeed);
            currentOffset.y = (position.y - deltaOffset.y)*distance/(deltaTime*currentBulletSpeed);
            currentOffset.z = (position.z - deltaOffset.z)*distance/(deltaTime*currentBulletSpeed);
            
            crutch.transform.position = position;

            crutch.transform.position += currentOffset;
            
            deltaOffset.x = position.x;
            deltaOffset.y = position.y;
            deltaOffset.z = position.z;
            deltaTime = 0f;
            prevTarget = target;
        }
        return crutch;
    }

    private void Update()
    {
        deltaTime += Time.deltaTime;
    }
}
