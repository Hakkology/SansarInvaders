using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteriousSansar : Sansar
{
    public float hiz = 5;
    public float atakHizi = 2;
    public float spawnHizi = 10;

    private Vector3 _direction;

    private Vector3 solTaraf;
    private Vector3 sagTaraf;

    private bool _isActive;
    // Start is called before the first frame update
    void Start()
    {
        solTaraf = Camera.main.ViewportToWorldPoint(Vector3.zero);
        sagTaraf = Camera.main.ViewportToWorldPoint(Vector3.one);

        InvokeRepeating(nameof(SpawnMysteriousSansar), 2, spawnHizi);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            transform.position += _direction * hiz * Time.deltaTime;

            if (transform.position.x < solTaraf.x || transform.position.x > sagTaraf.x)
            {
                _isActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void SpawnMysteriousSansar()
    {
        if (Random.value > 0.5f)
        {
            transform.position = new Vector3(solTaraf.x, 13, 0);
            _direction = Vector3.right;
        }
        else
        {
            transform.position = new Vector3(sagTaraf.x, 13, 0);
            _direction = Vector3.left;
        }

        _isActive = true;
        gameObject.SetActive(true);

        InvokeRepeating(nameof(SpecialMissileAttack), atakHizi, atakHizi / 2);
    }

    private void SpecialMissileAttack()
    {
        if (_isActive)
        {
            Projectile projectile = SpecialMissilePool.Instance.HavuzdanAl();
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                //projectile.direction = Vector3.down;
                projectile.gameObject.SetActive(true);
            }
        }
    }
}
