using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int damage;
    public int headshotDamage;
    public int velocity;

    public float bulletLife;
    public float spawnTime;

    GameObject enemy;
    EnemyHealth enemyHealthScript;

    static GameObject bullet;

    Rigidbody2D bulletrb;

    void Start()
    {
        spawnTime = Time.time;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            enemy = collision.gameObject;
            enemyHealthScript = enemy.GetComponent<EnemyHealth>();
            if (enemyHealthScript.shield > 0)
            {
                if (damage > enemyHealthScript.shield)
                {
                    float temp = damage - enemyHealthScript.shield;
                    enemyHealthScript.shield = 0;
                    enemyHealthScript.health -= temp;
                }
                else { enemyHealthScript.shield -= damage; }
            }
            else { enemyHealthScript.health -= damage; }
        }
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
        if (bulletLife != 0)
        {
            if (Time.time >= bulletLife + spawnTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
