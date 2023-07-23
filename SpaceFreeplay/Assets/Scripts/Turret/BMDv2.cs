using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BMDv2 : ShipModule
{
    [SerializeField] private float scatter;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationAngle;
    [SerializeField]private Transform target;
    [SerializeField] private Transform gunTower;

    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float currentTimeBetweenShot;

    [SerializeField] private float ammunition;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float burstLength;
    [SerializeField] private float currentBurstLength;
    [SerializeField] private float reloadTime;
    [SerializeField] private float currentReloadTime;


    [SerializeField] private bool canFire;
    [SerializeField] private Radar radar;
    [SerializeField] private BulletPool pool;

    private float currentBulletSpeed=15;
    [SerializeField] private GameObject crutch;
    [SerializeField] private FCSv1 fcs;
    [SerializeField] private AudioSource shootAudio;

    private void Start()
    {
        currentBulletSpeed = pool.GetBulletSpeed();
        shootAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Burst();
        RotateToTarget(target);
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
                    shootAudio.PlayOneShot(shootAudio.clip);
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
        canFire = false;
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

            crutch = fcs.FCSnewTarget(target, gunTower, currentBulletSpeed, crutch);
            
            Vector3 direction = (crutch.transform.position - gunTower.position).normalized;

            float crutchForRotationCheck = gunTower.localRotation.eulerAngles.y;
            if (crutchForRotationCheck > 180)
                crutchForRotationCheck -= 360;
            
            if (crutchForRotationCheck < maxRotationAngle && crutchForRotationCheck > -maxRotationAngle)
            {
                gunTower.rotation =
                    Quaternion.RotateTowards(gunTower.rotation, Quaternion.LookRotation(direction), rotationSpeed);
            }
            else
            {
                Debug.Log("Cant");
            }
               
            

            Vector3 targetDir = crutch.transform.position - gunTower.position;
            Vector3 forward = shootPoint.forward;
            float angleToTarget = -1 * Vector3.SignedAngle(targetDir, forward, Vector3.up);
            
            
            if (angleToTarget <= 0.01 && angleToTarget>=-0.01)
                canFire = true;
            else
                canFire = false;
        }
        else
        {
            NewTarget();
            canFire = false;
        }
           
    }

    void NewTarget()
    {
        if (target == null)
        {
            radar.TargetUpdate();
            target = radar.currentTarget;
        }
        
    }
    
}
