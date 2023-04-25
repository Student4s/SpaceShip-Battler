using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasedProjectile : Projectile
{
    [SerializeField] private float lifeTime = 1f;
    private float _currentLifeTime;
    [SerializeField] private float damage = 1f;

    void Start()
    {
        _currentLifeTime = lifeTime;
    }
    
    void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0)
        {
            _currentLifeTime = lifeTime;
            SetActive(false);
        }
        transform.Translate(0, 0, 1 * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<HitableModule>()!=null)
        {
            collision.collider.GetComponent<HitableModule>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
