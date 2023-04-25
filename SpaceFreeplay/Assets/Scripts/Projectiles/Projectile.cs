using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    public bool isActive=true;
    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
        isActive = state;
    }
    
    public float GetSpeed()
    {
        return speed;
    }
}
