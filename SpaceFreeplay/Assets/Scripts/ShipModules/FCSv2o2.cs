using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSv2o2 : FCSbased
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
            float rocketSpeed = target.GetComponent<Missile>().speed;
            var position = target.position;
            float distance = Vector3.Distance(position, gunTower.position);
            float delta = distance * rocketSpeed / currentBulletSpeed;

            crutch.transform.position = position;
            crutch.transform.rotation = target.rotation;;
            crutch.transform.Translate(0, 0, delta);

            deltaTime = 0f;
            prevTarget = target;
            deltaOffset.x = position.x;
            deltaOffset.y = position.y;
            deltaOffset.z = position.z;
            Debug.Log(deltaOffset);
        }
        else
        {

            float rocketSpeed = target.GetComponent<Missile>().speed;
            var position = target.position;
            float distance = Vector3.Distance(position, gunTower.position);
            
            currentOffset.x = (position.x - deltaOffset.x)*distance/(deltaTime*currentBulletSpeed);
            currentOffset.y = (position.y - deltaOffset.y)*distance/(deltaTime*currentBulletSpeed);
            currentOffset.z = (position.z - deltaOffset.z)*distance/(deltaTime*currentBulletSpeed);
            
            crutch.transform.position = position;
            crutch.transform.rotation = target.rotation;

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
