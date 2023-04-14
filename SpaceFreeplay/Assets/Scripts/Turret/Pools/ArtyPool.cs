using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyPool : MonoBehaviour
{
    [SerializeField] public List<BasedProjectile> bullets;
    [SerializeField] private BasedProjectile bulletStorage;

    void Start()
    {
        bullets = new List<BasedProjectile>();
    }

    public void SpawnBullet(Transform spawnPoint,Transform scatter)
    {
        bool isHave = false;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].isActive)
            {
                isHave = true;
                bullets[i].SetActive(true);
                bullets[i].GetComponent<Transform>().position = spawnPoint.position;
                bullets[i].GetComponent<Transform>().rotation = scatter.rotation;
                return;
            }
        }

        if (!isHave)
        {
            CreateNewBullet();
            bullets[bullets.Count-1].SetActive(true);
            bullets[bullets.Count-1].GetComponent<Transform>().position = spawnPoint.position;
            bullets[bullets.Count-1].GetComponent<Transform>().rotation = spawnPoint.rotation;
            return;
        }
    }

    public void CreateNewBullet()
    {
        BasedProjectile newBullet = Instantiate(bulletStorage);
        newBullet.SetActive(false);
        bullets.Add(newBullet);
    }
    
    public float GetBulletSpeed()
    {
        return bulletStorage.GetSpeed();
    }
}
