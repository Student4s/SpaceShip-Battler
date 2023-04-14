using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 1f;
    protected float _currentLifeTime;
    [SerializeField] protected float damage = 1f;
    public float speed = 1f;

    [SerializeField] protected float hp;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected Transform target;

    void Start()
    {
        _currentLifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentLifeTime<=0)
            Destroy(gameObject);

        _currentLifeTime -= Time.deltaTime;
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        
        if(hp<=0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
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