using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCSv3 : FCSv1
{
    [SerializeField] private Transform prevTarget;
    
    [SerializeField] private Vector3 dimension;
    [SerializeField] private float time;
    [SerializeField] private float time2;
    
    // Максимальное количество итераций подсчета
    [SerializeField] private int steps;
    [SerializeField] private int maxSteps;
    [SerializeField] private float accurecy;


    [SerializeField] private Vector3 currentOffset;

    private Vector3 offset;

    public override GameObject FCSnewTarget(Transform target, Transform gunTower, float currentBulletSpeed,
        GameObject crutch)
    {
        var position = target.position;
        if (prevTarget == null)
        {
            rocketSpeed = target.GetComponent<Missile>().speed;
            prevTarget = target;
            
            offset.x = target.position.x;
            offset.y = target.position.y;
            offset.z = target.position.z;
        }
        else
        {
            crutch.transform.position = target.position;
            dimension = GetRocketDimension(target); // вектор направления полета ракеты
            
            time = CalculateBulletFlyTime(crutch.transform, gunTower, currentBulletSpeed);//время полета пули к текущей точке размещения ракеты
            crutch.transform.Translate(dimension/Time.deltaTime*time);
            time2 = CalculateBulletFlyTime(crutch.transform, gunTower, currentBulletSpeed);

            crutch.transform.Translate(dimension/Time.deltaTime*(-time+time2));

           while (Mathf.Abs(time - time2) > accurecy && steps<=maxSteps)
            {
                time = CalculateBulletFlyTime(crutch.transform, gunTower, currentBulletSpeed);
                crutch.transform.Translate(dimension/Time.deltaTime*(-time+time2));
                time2 = CalculateBulletFlyTime(crutch.transform, gunTower, currentBulletSpeed);
                steps += 1;
            }
           
            offset.x = target.position.x;
            offset.y = target.position.y;
            offset.z = target.position.z;
        }

        steps = 0;
        return crutch;
    }

    Vector3 GetRocketDimension(Transform target)
    {
        Vector3 dimension = new Vector3();
        dimension.x = target.position.x- offset.x;
        dimension.y = target.position.y- offset.y;
        dimension.z = target.position.z- offset.z;


        return dimension;
    }

    float CalculateBulletFlyTime(Transform target, Transform gunTower, float currentBulletSpeed)
    {
        float distance = Vector3.Distance(target.position, gunTower.position);
        float t1 = distance / currentBulletSpeed;

        return t1;
    }
    
}