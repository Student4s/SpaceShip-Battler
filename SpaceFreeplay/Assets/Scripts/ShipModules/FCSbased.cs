using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSbased : MonoBehaviour
{
    public virtual GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {
        float rocketSpeed;

        if (target.GetComponent<Missile>() != null)
            rocketSpeed = target.GetComponent<Missile>().speed;
        else
            rocketSpeed = 0f;


        var position = target.position;
        float distance = Vector3.Distance(position, gunTower.position);
        float delta = distance * rocketSpeed / currentBulletSpeed;

        crutch.transform.position = position;
        crutch.transform.rotation = target.rotation;
        crutch.transform.Translate(0, 0, delta);

        return crutch;
    }
}