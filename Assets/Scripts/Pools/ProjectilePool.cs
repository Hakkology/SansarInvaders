using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    public Projectile laserPrefab;
    public int havuzBoyutu = 3;

    private Queue<Projectile> _pool;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HavuzuBaslat();
    }

    private void HavuzuBaslat()
    {
        _pool = new Queue<Projectile>();

        for (int i = 0; i < havuzBoyutu; i++)
        {
            Projectile projectile = Instantiate(laserPrefab, transform);
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

        Debug.Log("Havuz boþ kardeþim iþine git");
        return null;
    }
    public void HavuzaDonder(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _pool.Enqueue(projectile);
    }
}
