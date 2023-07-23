using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTargetToArty : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject currentTarget;
    
    public delegate void SetTatgetToComputer(GameObject target);
    public static event  SetTatgetToComputer Set;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1000, layer))
            {
                currentTarget = hit.collider.gameObject;
                Set(currentTarget);
            }
        }
    }

}
