using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : ProjectilePool
{
    public static LaserPool Instance { get; private set; }

    protected override void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        base.Awake();
    }
}
