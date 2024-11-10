using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMissilePool : ProjectilePool
{
    public static SpecialMissilePool Instance { get; private set; }

    protected override void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        base.Awake();
    }
}
