using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BMD : ShipModule
{
    [SerializeField] private float scatter;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAngle;
    private Transform target;
    [SerializeField] private string hits;
    [SerializeField] private Transform gunTower;

    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float currentTimeBetweenShot;

    [SerializeField] private float ammunition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform raycastPoint;

    [SerializeField] private float burstLength;
    [SerializeField] private float currentBurstLength;
    [SerializeField] private float reloadTime;
    [SerializeField] private float currentReloadTime;


    [SerializeField] private bool canFire;
    [SerializeField] private Radar radar;
    [SerializeField] private BMDpool pool;
    [SerializeField] private LayerMask layer;

    public delegate void UseModule(float heat);
    public static event  UseModule Use;
 

    void Update()
    {
        Burst();
        RotateToTarget(target);
        NewTarget();
    }

    void Burst()
    {
        if (ammunition > 0 && canFire)
        {
            if (currentBurstLength < burstLength)
            {
                if (currentTimeBetweenShot <= 0)
                {
                    Transform scatters = shootPoint;
                    scatters.Rotate(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
                    
                    pool.SpawnBullet(shootPoint, scatters);
                    Use(energyConsumePerUse);
                    currentTimeBetweenShot = timeBetweenShot;
                    currentBurstLength += 1;
                    ammunition -= 1;
                    
                    shootPoint.rotation = new Quaternion(0, 0, 0, 0);
                }
                else
                    currentTimeBetweenShot -= Time.deltaTime;
            }
            else
            {
                Reload();
            }
        }
    }

    void Reload()
    {
        if (currentReloadTime <= reloadTime)
        {
            currentReloadTime += Time.deltaTime;
        }
        else
        {
            currentBurstLength = 0;
            currentReloadTime = 0;
        }
    }

    void RotateToTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 direction = (target.position - gunTower.position).normalized;
            gunTower.rotation =
                Quaternion.RotateTowards(gunTower.rotation, Quaternion.LookRotation(direction), rotationSpeed);

            Debug.DrawRay(raycastPoint.position, shootPoint.forward * 100, Color.red);
            Ray ray = new Ray(raycastPoint.position, shootPoint.forward);

            if (Physics.Raycast(ray, out RaycastHit hit,1000, layer))
            {
                hits = hit.collider.name;
                if (hit.collider.CompareTag("Rocket"))
                    canFire = true;
                else
                    canFire = false;
            }
        }
    }

    void NewTarget()
    {
        target = radar.currentTarget;
    }
    
}
