using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRocketLauncher : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAngle;
    [SerializeField] private Transform target;
    [SerializeField] private Transform gunTower;
    [SerializeField] private string hits;

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
        Vector3 direction = (target.position - gunTower.position).normalized;
        gunTower.rotation =
            Quaternion.RotateTowards(gunTower.rotation, Quaternion.LookRotation(direction), rotationSpeed);

        Debug.DrawRay(raycastPoint.position, raycastPoint.forward * 100, Color.red);
        Ray ray = new Ray(raycastPoint.position, raycastPoint.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit,1000, layer))
        {
            hits = hit.collider.name;
            if (hit.collider.CompareTag("Point"))
                canFire = true;
            else
                canFire = false;
        }
    }
}