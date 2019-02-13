using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ak47Bullet : MonoBehaviour {

    int damage;
    int velocity;

    float bulletLife;
    float spawnTime;

    static GameObject bullet;

    Rigidbody2D bulletrb;

    void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            
        }
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
    }
}
