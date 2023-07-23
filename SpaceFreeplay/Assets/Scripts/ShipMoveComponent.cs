using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveComponent : MonoBehaviour
{
    [SerializeField] private float straightSpeed;
    [SerializeField] private float maxStraightSpeed;
    [SerializeField] private float rotateSpeed;
    //[SerializeField] private float maxRotateSpeed;

    [SerializeField] private float speedAcceleration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            if (straightSpeed <= maxStraightSpeed)
                straightSpeed = Math.Clamp(straightSpeed + speedAcceleration * Time.fixedDeltaTime, 0,
                    maxStraightSpeed);
        }
        if (Input.GetKey("s"))
        {
            if (straightSpeed >= 0)
                straightSpeed = Math.Clamp(straightSpeed - speedAcceleration/2 * Time.fixedDeltaTime, 0,
                    maxStraightSpeed);
        }

       
        if (Input.GetKey("a"))
        {
            gameObject.transform.Rotate(0,-rotateSpeed* Time.fixedDeltaTime,0);
        }
        if (Input.GetKey("d"))
        {
            gameObject.transform.Rotate(0,rotateSpeed* Time.fixedDeltaTime,0);
        }
        
       
        if (Input.GetKey("q"))
        {
            gameObject.transform.Rotate(0,0,rotateSpeed* Time.fixedDeltaTime);
        }
        if (Input.GetKey("e"))
        {
            gameObject.transform.Rotate(0,0,-rotateSpeed* Time.fixedDeltaTime);
        }
        
        
        gameObject.transform.Translate(straightSpeed* Time.fixedDeltaTime,0,0);
    }
}
