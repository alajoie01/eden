using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerPatrol : MonoBehaviour {

    GameObject player;
    GameObject firePointLeft;
    GameObject firePointRight;

    GameObject healthBar;
    GameObject shieldBar;

    EnemyHealth healthScript;

    public GameObject buzzerBullet = null;

    Transform legsOb;
    Transform bodyOb;
    Transform headOb;

    private Animator legs;
    private Animator body;
    private Animator head;

    public int range = 150;
    public int bulletCount = 0;
    public int bulletVelocity = 300;

    public int damage = 10;
    public float bulletLife = 0;

    public float health = 50;
    public float shield = 20;
    public float maxHealth = 50;
    public float maxShield = 20;
    float shieldScale;
    float healthScale;

    public bool readyToFire = true;
    public bool fireMode = false;

    public float moveSpeed;
    public float direction = 1f;
    public float patrolTime = 3f;
    public float lastSwitch = 0f;
    public float enterRange;
    public float fireDelay = 1f;
    public float aimDirection;
    public float fireRate = 1f;
    public float lastShot = 0;

    public bool lookLeft;
    public bool lookRight;

	// Use this for initialization
	void Start () {

        healthScript = gameObject.GetComponent<EnemyHealth>();

        healthScript.health = health;
        healthScript.shield = shield;
        healthScript.maxHealth = maxHealth;
        healthScript.maxShield = maxShield;

        player = GameObject.Find("Player");

        firePointLeft = GameObject.Find("firePointLeft");
        firePointRight = GameObject.Find("firePointRight");

        shieldBar = GameObject.Find("EnemyShield");
        healthBar = GameObject.Find("EnemyHealth");

        // Setting up sprites and animators
        legsOb = transform.Find("buzzerLegs");
        legs = legsOb.GetComponent<Animator>();

        bodyOb = transform.Find("buzzerBody");
        body = bodyOb.GetComponent<Animator>();

    }

    public void CollisionDetected(GameObject player)
    {
        healthScript.health = 0;
        healthScript.shield = 0;
        Player p = player.GetComponent<Player>();
        p.rb.AddForce(Vector2.up * p.jumpPower);
    }

    // Update is called once per frame
    void Update () {

        body.SetBool("Left", lookLeft);
        body.SetBool("Right", lookRight);

        // HealthScript

        health = healthScript.health;
        shield = healthScript.shield;
        maxHealth = healthScript.maxHealth;
        maxShield = healthScript.maxShield;

        // Patrolling back and forth
        transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime * direction);

        if (Time.time > patrolTime + lastSwitch)
        {
            direction *= -1;
            lastSwitch = Time.time;
        }

        // Flipping legs based on direction
        if (direction == -1)
        {
            legsOb.localScale = new Vector3(-1, 1, 1);
        }
        if (direction == 1)
        {
            legsOb.localScale = new Vector3(1, 1, 1);
        }

        // Looking left and right
        if (player.transform.position.x < bodyOb.transform.position.x &&
            player.transform.position.x > bodyOb.transform.position.x - range &&
            fireMode == false)
        {
            aimDirection = -1;
            lookLeft = true;
            lookRight = false;
        }
        if (player.transform.position.x > bodyOb.transform.position.x &&
            player.transform.position.x < bodyOb.transform.position.x + range &&
            fireMode == false)
        {
            aimDirection = 1;
            lookRight = true;
            lookLeft = false;
        }

        // In and out of range
        if ((bodyOb.transform.position.y - 4) < player.transform.position.y && 
            player.transform.position.y < (bodyOb.transform.position.y + 4))
        {
            if (player.transform.position.x < bodyOb.transform.position.x &&
                player.transform.position.x > bodyOb.transform.position.x - range)
            {
                // Firing left
                if (readyToFire)
                {
                    enterRange = Time.time;
                    lastShot = Time.time + fireDelay;
                    bulletCount = 0;
                    fireMode = true;
                    readyToFire = false;
                }
                
            }
            else if (player.transform.position.x > bodyOb.transform.position.x &&
            player.transform.position.x < bodyOb.transform.position.x + range)
            {
                // Firing right
                if (readyToFire)
                {
                    enterRange = Time.time;
                    lastShot = Time.time + fireDelay;
                    bulletCount = 0;
                    fireMode = true;
                    readyToFire = false;
                }
            }
        }

        // Firing
        if (fireMode)
        {
            if (Time.time > lastShot + fireRate &&
                bulletCount < 2)
            {
                if (aimDirection == -1)
                {
                    GameObject bullet = Instantiate(buzzerBullet, firePointLeft.transform.position,
                                                    Quaternion.Euler(0f, 0f, 180f));
                    EnemyBulletScript bScript = bullet.GetComponent<EnemyBulletScript>();
                    bScript.damage = damage;
                    bScript.headshotDamage = damage;
                    bScript.velocity = bulletVelocity;
                    bScript.bulletLife = bulletLife;
                    lastShot = Time.time;
                    bulletCount++;
                }
                if (aimDirection == 1)
                {
                    GameObject bullet = Instantiate(buzzerBullet, firePointRight.transform.position,
                                                    Quaternion.Euler(0f, 0f, 0f));
                    EnemyBulletScript bScript = bullet.GetComponent<EnemyBulletScript>();
                    bScript.damage = damage;
                    bScript.headshotDamage = damage;
                    bScript.velocity = bulletVelocity;
                    bScript.bulletLife = bulletLife;
                    lastShot = Time.time;
                    bulletCount++;
                }
            }
            if (Time.time > enterRange + 5)
            {
                readyToFire = true;
                fireMode = false;
                bulletCount = 0;
            }

        }

        // Health/Shield Bars
        healthScale = health / maxHealth;
        shieldScale = shield / maxShield;

        healthBar.transform.localScale = new Vector3(healthScale, 1, 1);
        shieldBar.transform.localScale = new Vector3(shieldScale, 1, 1);

        // Health
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

}
