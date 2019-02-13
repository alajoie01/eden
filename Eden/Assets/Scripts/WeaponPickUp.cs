using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

    public Weapon weapon;

    string realName;
    string weaponName;

    GameObject playerWeapon = null;
    SpriteRenderer spriteRenderer = null;
    GameObject firePoint = null;
    GameObject flash = null;
    GameObject bullet = null;

    int damage;
    int headshotDamage;
    int magAmmo;
    int totalAmmo;
    int bulletVelocity;

    float fireRate;
    float flashTime;
    float accuracy;
    float bulletLife;
    float reloadSpeed;

    string weaponType;
    int burstCount;
    int weaponTier;

    int currentMagAmmo;
    int currentTotalAmmo;

    // Use this for initialization
    void Start()
    {
        realName = weapon.realName;
        weaponName = weapon.weaponName;
        flash = weapon.flash;
        bullet = weapon.bullet;
        damage = weapon.damage;
        headshotDamage = weapon.headshotDamage;
        magAmmo = weapon.magAmmo;
        totalAmmo = weapon.totalAmmo;
        bulletVelocity = weapon.bulletVelocity;
        fireRate = weapon.fireRate;
        flashTime = weapon.flashTime;
        accuracy = weapon.accuracy;
        weaponType = weapon.weaponType;
        burstCount = weapon.burstCount;
        weaponTier = weapon.weaponTier;
        bulletLife = weapon.bulletLife;
        reloadSpeed = weapon.reloadSpeed;
        currentMagAmmo = weapon.currentMagAmmo;
        currentTotalAmmo = weapon.currentTotalAmmo;

        // Setting up WeaponObject GameObjects/SpriteRenderers
        playerWeapon = GameObject.Find(weaponName);
        spriteRenderer = playerWeapon.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        firePoint = GameObject.Find(weaponName + "_firepoint");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        WeaponObject weapon = new WeaponObject(realName, spriteRenderer, bullet, firePoint, damage,
                        headshotDamage, magAmmo, totalAmmo, bulletVelocity, fireRate, accuracy,
                        flash, flashTime, weaponType, burstCount, weaponTier, bulletLife, reloadSpeed,
                        currentMagAmmo, currentTotalAmmo);

        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
            GameObject player = GameObject.Find("Player");
            Player other = (Player)player.GetComponent(typeof(Player));
            other.PickUpWeapon(weapon);
        }
    }
}
