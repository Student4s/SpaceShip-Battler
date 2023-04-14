using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketV2 : Missile
{
    [SerializeField] private float sideSpeed = 1f;
    [SerializeField] private float spiralSpeed = 1f;


    void Update()
    {
       
        transform.Translate(0,0,speed*Time.deltaTime);
        transform.Translate(sideSpeed*Time.deltaTime,0,0);
        transform.Rotate(0,0,spiralSpeed);
       
    }

    
}
