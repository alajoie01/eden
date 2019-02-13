using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    //Gameplay Variables

    public float health;
    public float shield;
    public float maxHealth;
    public float maxShield;

    public float speed = 1500f;
    public float sprintSpeed = 3000f;
    public float maxSpeed = 100f;
    public float maxSprintSpeed = 200f;
    public float jumpPower = 120000f;

    public float startTime;

    public int switchCount = 1;

    public bool grounded;
    public bool backwards;
    public bool holdA;
    public bool holdD;
    public bool oPTC = true;

    public Rigidbody2D rb;
    
    private Animator anim;

    Transform playerBody;
    Transform playerArm;

    static GameObject armStraight;
    static GameObject armBent;
    static GameObject armBentDown;
    public SpriteRenderer armStraightsr;
    public SpriteRenderer armBentsr;
    public SpriteRenderer armBentDownsr;

    Collider2D bodyCol;
    Collider2D feetCol;

    GameObject HUD;
    Bar hudScript;

    public List<WeaponObject> weaponInventory = new List<WeaponObject>();
    public List<WeaponObject> weaponEquipInventory = new List<WeaponObject>();

    public WeaponObject currentWeapon1;

    static SpriteRenderer currentWeapon1sr;

    float disable = -1f;
    Collider2D pTC = null;
    bool passedThrough = false;
    bool canPress = true;

    // Use this for initialization
    void Start()
    {

        // Gameplay Variables
        health = 100;
        shield = 100;
        maxHealth = 100;
        maxShield = 100;

        // Colliders
        GameObject pTC = GameObject.Find("PassThroughCollider");
        feetCol = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
        bodyCol = gameObject.GetComponent<Collider2D>();

        // Player Components
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerBody = transform.Find("PlayerBody");
        anim = playerBody.GetComponent<Animator>();

        // Setting up PlayerArm SpriteRenderers
        armStraight = GameObject.Find("PlayerArm");
        armBent = GameObject.Find("PlayerArmBent");
        armBentDown = GameObject.Find("PlayerArmBentDown");
        armStraightsr = armStraight.GetComponent<SpriteRenderer>();
        armBentsr = armBent.GetComponent<SpriteRenderer>();
        armBentDownsr = armBentDown.GetComponent<SpriteRenderer>();

        // HUD Components
        HUD = GameObject.Find("HUD");
        hudScript = HUD.GetComponent<Bar>();

    }

    // The function for picking up weapons- if there is not two equiped weapons, the picked up weapon
    // will be equiped by default
    public void PickUpWeapon(WeaponObject weapon)
    {
        weaponInventory.Add(weapon);
        Debug.Log(weaponInventory.Count);
        if (weaponEquipInventory.Count < 2)
        {
            weaponEquipInventory.Add(weapon);
            Debug.Log(weaponEquipInventory.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Setting up HUD variables
        hudScript.health = health;
        hudScript.shield = shield;
        hudScript.maxHealth = maxHealth;
        hudScript.maxShield = maxShield;

        // Setting up variables for animations
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Backwards", backwards);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        GameObject.Find("speechText").GetComponent<autoType>();

        // Flipping the character
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            playerBody.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            playerBody.localScale = new Vector3(-1, 1, 1);
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded == true)
            {
                rb.AddForce(Vector2.up * jumpPower);
            }
        }

        // Passing Through 2-Way Floors
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("pTCMethod");
        }

        // Switching equiped weapons by using number buttons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchCount = 1;
            Debug.Log("switchCount = " + switchCount);
            startTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchCount = 2;
            Debug.Log("switchCount = " + switchCount);
            startTime = Time.time;
        }

        // Switching equiped weapons by using scroll wheel **NEEDS A REWORK**
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (switchCount == 1)
            {
                switchCount = 2;
                Debug.Log("switchCount = " + switchCount);
                startTime = Time.time;
            }
            if (switchCount == 2)
            {
                switchCount = 1;
                Debug.Log("switchCount = " + switchCount);
                startTime = Time.time;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (switchCount == 2)
            {
                switchCount = 1;
                Debug.Log("switchCount = " + switchCount);
                startTime = Time.time;
            }
            if (switchCount == 1)
            {
                switchCount = 2;
                Debug.Log("switchCount = " + switchCount);
                startTime = Time.time;
            }
        }

        // Equiping weapons
        if (weaponEquipInventory.Count == 0)
        {
            armStraightsr.enabled = false;
            armBentDownsr.enabled = false;
            armBentsr.enabled = true;
        }

        if (weaponEquipInventory.Count > 0)
        {
            WeaponObject weaponPrimary = weaponEquipInventory[0];
            SpriteRenderer weaponPrimarysr = weaponPrimary.getSprite();
            weaponPrimarysr.enabled = false;
            if (switchCount == 1)
            {
                weaponPrimarysr.enabled = true;
                if (weaponPrimary.getWeaponType().Equals("ar"))
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = true;
                    armBentDownsr.enabled = false;
                }
                else if (weaponPrimary.getWeaponType().Equals("handgun"))
                {
                    armStraightsr.enabled = true;
                    armBentsr.enabled = false;
                    armBentDownsr.enabled = false;
                }
                else if (weaponPrimary.getWeaponType().Equals("shotgun"))
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = false;
                    armBentDownsr.enabled = true;
                }
                else
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = true;
                    armBentDownsr.enabled = false;
                }
            }
            if (switchCount == 2)
            {
                armStraightsr.enabled = false;
                armBentsr.enabled = true;
                armBentDownsr.enabled = false;
            }
        }

        if (weaponEquipInventory.Count > 1)
        {
            WeaponObject weaponSecondary = weaponEquipInventory[1];
            SpriteRenderer weaponSecondarysr = weaponSecondary.getSprite();
            weaponSecondarysr.enabled = false;
            if (switchCount == 2)
            {
                weaponSecondarysr.enabled = true;
                if (weaponSecondary.getWeaponType().Equals("ar"))
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = true;
                    armBentDownsr.enabled = false;
                }
                else if (weaponSecondary.getWeaponType().Equals("handgun"))
                {
                    armStraightsr.enabled = true;
                    armBentsr.enabled = false;
                    armBentDownsr.enabled = false;
                }
                else if (weaponSecondary.getWeaponType().Equals("shotgun"))
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = false;
                    armBentDownsr.enabled = true;
                }
                else
                {
                    armStraightsr.enabled = false;
                    armBentsr.enabled = true;
                    armBentDownsr.enabled = false;
                }
            }
        }
    }

    // Passing through 2 way floors
    private IEnumerator pTCMethod()
    {
        Debug.Log("started");
        Physics2D.IgnoreLayerCollision(8, 14, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreLayerCollision(8, 14, false);
    }

    private void FixedUpdate()
    {

        // Artificial friction
        Vector3 easeVelocity = rb.velocity;
        easeVelocity.x *= 0.954678703f;
        easeVelocity.z = 0f;
        easeVelocity.y = rb.velocity.y;

        if (grounded)
        {
            rb.velocity = easeVelocity;
        }

        float x = Input.GetAxis("Horizontal");

        if (Input.GetKey("left shift"))
        {

            rb.AddForce((Vector2.right * sprintSpeed) * x);

            if (rb.velocity.x > maxSprintSpeed)
            {
                rb.velocity = new Vector2(maxSprintSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < -maxSprintSpeed)
            {
                rb.velocity = new Vector2(-maxSprintSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.AddForce((Vector2.right * speed) * x);

            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
        }

        float y = Input.GetAxis("Vertical");

    }
}
