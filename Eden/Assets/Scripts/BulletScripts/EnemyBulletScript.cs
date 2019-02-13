using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    public int damage;
    public int headshotDamage;
    public int velocity;

    public float bulletLife;
    public float spawnTime;

    GameObject player;
    Player playerScript;

    static GameObject bullet;

    Rigidbody2D bulletrb;

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        spawnTime = Time.time;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerScript.shield > 0)
            {
                playerScript.shield -= damage;
            }
            else { playerScript.health -= damage; }
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

