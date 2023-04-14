using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComputer : MonoBehaviour
{
    [SerializeField] private BaseTurret[] allTurrets;
    [SerializeField] private Text targetName;
    [SerializeField] private GameObject currentTarget;
    void Start()
    {
        SelectTargetToArty.Set += SetTarget;
    }

    void SetTarget(GameObject target)
    {
        currentTarget = target;
        targetName.text = currentTarget.name;
    }

    public void FireUp()
    {
        for (int i = 0; i <= allTurrets.Length - 1; i++)
        {
            allTurrets[i].SelectTarget(currentTarget.transform);
        }
    }

    private void OnDisable()
    {
        SelectTargetToArty.Set -= SetTarget;
    }
}
