using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour {

    string weaponName;

    SpriteRenderer sprite;

    GameObject bullet;
    GameObject firepoint;
    GameObject flash;

    int damage;
    int headshotDamage;
    int magAmmo;
    int currentMagAmmo;
    int currentTotalAmmo;
    int totalAmmo;
    int bulletVelocity;

    float fireRate;
    float flashTime;
    float accuracy;

    string weaponType;
    int burstCount;
    int weaponTier;
    float bulletLife;
    float reloadSpeed;

    public WeaponObject(string weaponName, SpriteRenderer sprite, GameObject bullet, GameObject firepoint, int damage,
                        int headshotDamage, int magAmmo, int totalAmmo, int bulletVelocity, float fireRate, float accuracy,
                        GameObject flash, float flashTime, string weaponType, int burstCount, int weaponTier,
                        float bulletLife, float reloadSpeed, int currentMagAmmo, int currentTotalAmmo)
    {
        this.weaponName = weaponName;
        this.sprite = sprite;
        this.bullet = bullet;
        this.firepoint = firepoint;
        this.damage = damage;
        this.headshotDamage = headshotDamage;
        this.magAmmo = magAmmo;
        this.totalAmmo = totalAmmo;
        this.bulletVelocity = bulletVelocity;
        this.fireRate = fireRate;
        this.accuracy = accuracy;
        this.flash = flash;
        this.flashTime = flashTime;
        this.weaponType = weaponType;
        this.burstCount = burstCount;
        this.weaponTier = weaponTier;
        this.bulletLife = bulletLife;
        this.reloadSpeed = reloadSpeed;
        this.currentMagAmmo = currentMagAmmo;
        this.currentTotalAmmo = currentTotalAmmo;
    }

    public string getWeaponName()
    {
        return weaponName;
    }

    public SpriteRenderer getSprite()
    {
        return sprite;
    }

    public GameObject getBullet()
    {
        return bullet;
    }

    public GameObject getFirepoint()
    {
        return firepoint;
    }

    public int getDamage()
    {
        return damage;
    }

    public int getHeadshotDamage()
    {
        return headshotDamage;
    }

    public int getMagAmmo()
    {
        return magAmmo;
    }

    public int getTotalAmmo()
    {
        return totalAmmo;
    }

    public int getBulletVelocity()
    {
        return bulletVelocity;
    }

    public float getFireRate()
    {
        return fireRate;
    }

    public float getAccuracy()
    {
        return accuracy;
    }

    public GameObject getFlash()
    {
        return flash;
    }

    public float getFlashTime()
    {
        return flashTime;
    }

    public string getWeaponType()
    {
        return weaponType;
    }

    public int getBurstCount()
    {
        return burstCount;
    }

    public int getWeaponTier()
    {
        return weaponTier;
    }

    public float getBulletLife()
    {
        return bulletLife;
    }

    public float getReloadSpeed()
    {
        return reloadSpeed;
    }

    public void setMagAmmo(int mAmmo)
    {
        magAmmo = mAmmo;
    }

    public int getCurrentMagAmmo()
    {
        return currentMagAmmo;
    }

    public int getCurrentTotalAmmo()
    {
        return currentTotalAmmo;
    }

    public void setCurrentMagAmmo(int cMAmmo)
    {
        currentMagAmmo = cMAmmo;
    }

    public void setCurrentTotalAmmo(int cTAmmo)
    {
        currentTotalAmmo = cTAmmo;
    }
}
