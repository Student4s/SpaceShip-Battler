using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSvoid: FCSv1
{
    public override GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {
        crutch.transform.position = target.position;
        return crutch;
    }
}
