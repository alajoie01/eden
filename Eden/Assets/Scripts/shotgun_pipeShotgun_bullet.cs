using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun_pipeShotgun_bullet : MonoBehaviour
{

    int damage;
    int velocity;

    float bulletLife;
    float spawnTime;

    static GameObject bullet;

    Rigidbody2D bulletrb;

    void Start()
    {
        spawnTime = Time.time;
        damage = 10;
        velocity = 360;

        bulletLife = 0.2f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);

        if (Time.time >= bulletLife + spawnTime)
        {
            Destroy(gameObject);
        }
    }
}