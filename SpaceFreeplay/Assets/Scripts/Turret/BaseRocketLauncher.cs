using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRocketLauncher : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private Transform target;
    [SerializeField] private Transform gunTower;

    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float currentTimeBetweenShot;

    [SerializeField] private float ammunition;
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform[] shootPoint;
    [SerializeField] private Transform raycastPoint;

    [SerializeField] private int burstLength;
    [SerializeField] private int currentBurstLength;
    [SerializeField] private float reloadTime;
    [SerializeField] private float currentReloadTime;

    [SerializeField] private LayerMask layer;
    [SerializeField] private bool canFire;

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
                    GameObject A = Instantiate(rocket, shootPoint[currentBurstLength].position,
                        shootPoint[currentBurstLength].rotation);
                    A.GetComponent<DefRocket>().SetTarget(target);
                    currentTimeBetweenShot = timeBetweenShot;
                    currentBurstLength += 1;
                    ammunition -= 1;
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

            Vector3 direction = (target.transform.position - gunTower.position).normalized;

            float crutchForRotationCheck = gunTower.localRotation.eulerAngles.y;
            if (crutchForRotationCheck > 180)
                crutchForRotationCheck -= 360;
            
            if (crutchForRotationCheck < maxRotationAngle && crutchForRotationCheck > -maxRotationAngle)
            {
                gunTower.rotation =
                    Quaternion.RotateTowards(gunTower.rotation, Quaternion.LookRotation(direction), rotationSpeed); 
                Debug.Log("Fsdf");
            }
            else
            {
                Debug.Log("Cant");
            }
            Vector3 targetDir = target.transform.position - gunTower.position;
            Vector3 forward = shootPoint[0].forward;
            float angleToTarget = -1 * Vector3.SignedAngle(targetDir, forward, Vector3.up);
            
            
            if (angleToTarget <= 0.1 && angleToTarget>=-0.1)
                canFire = true;
            else
                canFire = false;
        }
        else
            canFire = false;
        }


}