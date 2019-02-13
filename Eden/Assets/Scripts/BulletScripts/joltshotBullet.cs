using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joltshotBullet : MonoBehaviour {

    int damage;
    int velocity;

    static GameObject ar_ak47_bullet;

    Rigidbody2D ar_ak47_bulletrb;

    void Start()
    {
        damage = 30;
        velocity = 700;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
    }
}
