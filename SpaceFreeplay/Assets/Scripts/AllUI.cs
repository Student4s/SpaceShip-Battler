using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllUI : MonoBehaviour
{
        [SerializeField] private BaseTurret[] allTurrets;
        [SerializeField] private Text targetName;
        [SerializeField] private GameObject currentTarget;
        
        [SerializeField] private Text HP;
        [SerializeField] private float maxHP;
        [SerializeField] private float currentHP;
        [SerializeField] private Text currentAmmo;
        [SerializeField] private int ArtyAmmo;
        [SerializeField] private int BMDAmmo;
        [SerializeField] private int RocketAmmo;
        [SerializeField] private Text speed;
        [SerializeField] private float currentSpeed;
        
        [SerializeField] private Text radarTargetList;
        
        void Start()
        {
            SelectTargetToArty.Set += SetTarget;
            BaseTurret.MinusAmmo += UpdateAmmoCount;
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

        void UpdateAmmoCount(string ammunitionType)
        {
            if (ammunitionType == "arty")
                ArtyAmmo -= 1;
            if (ammunitionType == "bmd")
                BMDAmmo -= 1;
            if (ammunitionType == "rocket")
                RocketAmmo -= 1;
            currentAmmo.text = ArtyAmmo.ToString() + "/" + BMDAmmo.ToString() + "/" + RocketAmmo.ToString();
        }
    
        private void OnDisable()
        {
            SelectTargetToArty.Set -= SetTarget;
            BaseTurret.MinusAmmo -= UpdateAmmoCount;
        }
}
