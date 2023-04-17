using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifeTime = 1f;
    private float _currentLifeTime;
    [SerializeField] private float damage = 1f;
    public bool isActive=true;
    void Start()
    {
        _currentLifeTime = lifeTime;
        transform.parent = transform.root;
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
    
    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
        isActive = state;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.collider.GetComponent<Missile>()!=null)
        {
            collision.collider.GetComponent<Missile>().GetDamage(damage);
        }
        SetActive(false);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
