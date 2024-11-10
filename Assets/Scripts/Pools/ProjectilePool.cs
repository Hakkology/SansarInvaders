using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectilePool : MonoBehaviour
{
    //public static ProjectilePool Instance { get; private set; }
    public Projectile projectilePrefab;
    public int havuzBoyutu = 3;

    protected Queue<Projectile> _pool;
    protected virtual void Awake()
    {
        //if (Instance == null)
        //    Instance = this;
        //else
        //    Destroy(gameObject);

        HavuzuBaslat();
    }

    private void HavuzuBaslat()
    {
        _pool = new Queue<Projectile>();

        for (int i = 0; i < havuzBoyutu; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform);
            projectile.gameObject.SetActive(false);
            projectile.destroyed += HavuzaDonder;
            _pool.Enqueue(projectile);
        }
    }

    public Projectile HavuzdanAl()
    {
        if (_pool.Count >0)
        {
            Projectile projectile = _pool.Dequeue();
            projectile.gameObject.SetActive(true);
            return projectile; 
        }

        return null;
    }
    public void HavuzaDonder(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _pool.Enqueue(projectile);
    }

}
