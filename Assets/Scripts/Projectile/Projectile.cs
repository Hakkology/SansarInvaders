using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public Action<Projectile> destroyed;


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        destroyed.Invoke(this);
        //ProjectilePool.Instance.HavuzaDonder(this);
    }
}
