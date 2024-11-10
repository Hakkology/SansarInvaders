using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public Projectile _laserPrefab;
    public SansarGrid SansarGrid;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Projectile laser = LaserPool.Instance.HavuzdanAl();
        if (laser != null)
        {
            laser.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            GameState.Instance.ReduceHealth();
            //GameState.Instance.ResetBoard();
        }
    }
}
