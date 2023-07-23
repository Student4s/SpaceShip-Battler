using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSv1 : MonoBehaviour
{
    [SerializeField] private GameObject currentTarget;
    [SerializeField] protected float rocketSpeed = 0f;
    
    public virtual GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {

        if (currentTarget == null)
        {
            rocketSpeed = target.GetComponent<Missile>().speed;
            currentTarget = target.gameObject;
        }


        var position = target.position;
        float distance = Vector3.Distance(position, gunTower.position);
        float delta = distance * rocketSpeed / currentBulletSpeed;

        crutch.transform.position = position;
        crutch.transform.rotation = target.rotation;
        crutch.transform.Translate(0, 0, delta);

        return crutch;
    }
}