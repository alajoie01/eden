using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour {

    static Random rnd = new Random();

    public int rotationOffset = 90;

    float startTime;

    GameObject reloadBar;

    private Player player;

    public bool allowFire = true;
    public bool readyToFire = true;
    public bool reloading = false;

    float lastShot = 0f;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        reloadBar = GameObject.Find("Reload Bar");
    }

    // Update is called once per frame
    void Update () {

        startTime = player.startTime;

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        if (player.weaponEquipInventory.Count == 0)
        {
            reloadBar.transform.localScale = new Vector3(0, 0, 0);
        }
        else if (player.weaponEquipInventory.Count == 1)
        {
            if (player.switchCount == 1)
            {
                if (player.weaponEquipInventory[0].getCurrentMagAmmo() > 0)
                {
                    reloadBar.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else if (player.switchCount == 2)
            {
                reloadBar.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        else if (player.weaponEquipInventory.Count == 2)
        {
            if (player.switchCount == 1 && player.weaponEquipInventory[0].getCurrentMagAmmo() > 0)
            {
                reloadBar.transform.localScale = new Vector3(0, 0, 0);
            }
            else if (player.switchCount == 2 && player.weaponEquipInventory[1].getCurrentMagAmmo() > 0)
            {
                reloadBar.transform.localScale = new Vector3(0, 0, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            readyToFire = true;
        }

        if (player.switchCount == 1 && player.weaponEquipInventory.Count > 0)
        {
            if (player.weaponEquipInventory[0].getCurrentMagAmmo() == 0)
            {
                float scale = (1 - ((Time.time - startTime) / player.weaponEquipInventory[0].getReloadSpeed()));
                reloadBar.transform.localScale = new Vector3(scale, 1, 1);
                if (Time.time >= (startTime + player.weaponEquipInventory[0].getReloadSpeed()))
                {
                    player.weaponEquipInventory[0].setCurrentMagAmmo(player.weaponEquipInventory[0].getMagAmmo());
                }
            }
        }
        else if (player.switchCount == 2 && player.weaponEquipInventory.Count > 1)
        {
            if (player.weaponEquipInventory[1].getCurrentMagAmmo() == 0)
            {
                float scale = (1 - ((Time.time - startTime) / player.weaponEquipInventory[1].getReloadSpeed()));
                reloadBar.transform.localScale = new Vector3(scale, 1, 1);
                if (Time.time >= (startTime + player.weaponEquipInventory[1].getReloadSpeed()))
                {
                    player.weaponEquipInventory[1].setCurrentMagAmmo(player.weaponEquipInventory[1].getMagAmmo());
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (player.switchCount == 1 && player.weaponEquipInventory.Count > 0)
            {
                if (player.weaponEquipInventory[0].getCurrentMagAmmo() > 0)
                {
                    float fireRate = player.weaponEquipInventory[0].getFireRate();
                    if (player.weaponEquipInventory[0].getWeaponType().Equals("ar"))
                    {
                        if (Time.time > fireRate + lastShot)
                        {
                            GameObject flash = Instantiate(player.weaponEquipInventory[0].getFlash(),
                                player.weaponEquipInventory[0].getFirepoint().transform.position,
                                Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[0].getAccuracy();
                            GameObject bullet = Instantiate(player.weaponEquipInventory[0].getBullet(),
                                player.weaponEquipInventory[0].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                            BulletScript bScript = bullet.GetComponent<BulletScript>();
                            bScript.damage = player.weaponEquipInventory[0].getDamage();
                            bScript.headshotDamage = player.weaponEquipInventory[0].getHeadshotDamage();
                            bScript.velocity = player.weaponEquipInventory[0].getBulletVelocity();
                            bScript.bulletLife = player.weaponEquipInventory[0].getBulletLife();
                            lastShot = Time.time;
                            Destroy(flash, player.weaponEquipInventory[0].getFlashTime());
                            player.weaponEquipInventory[0].setCurrentMagAmmo(
                                player.weaponEquipInventory[0].getCurrentMagAmmo() - 1);
                            if (player.weaponEquipInventory[0].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }
                        }
                    }
                    else if (player.weaponEquipInventory[0].getWeaponType().Equals("handgun"))
                    {
                        if (Time.time > fireRate + lastShot && readyToFire)
                        {
                            readyToFire = false;
                            GameObject flash = Instantiate(player.weaponEquipInventory[0].getFlash(),
                                player.weaponEquipInventory[0].getFirepoint().transform.position,
                                   Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[0].getAccuracy();
                            GameObject bullet = Instantiate(player.weaponEquipInventory[0].getBullet(),
                                player.weaponEquipInventory[0].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                            BulletScript bScript = bullet.GetComponent<BulletScript>();
                            bScript.damage = player.weaponEquipInventory[0].getDamage();
                            bScript.headshotDamage = player.weaponEquipInventory[0].getHeadshotDamage();
                            bScript.velocity = player.weaponEquipInventory[0].getBulletVelocity();
                            bScript.bulletLife = player.weaponEquipInventory[0].getBulletLife();
                            lastShot = Time.time;
                            Destroy(flash, player.weaponEquipInventory[0].getFlashTime());
                            player.weaponEquipInventory[0].setCurrentMagAmmo(
                                player.weaponEquipInventory[0].getCurrentMagAmmo() - 1);
                            if (player.weaponEquipInventory[0].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }

                        }
                    }
                    else if (player.weaponEquipInventory[0].getWeaponType().Equals("shotgun"))
                    {
                        if (Time.time > fireRate + lastShot && readyToFire)
                        {
                            readyToFire = false;
                            GameObject flash = Instantiate(player.weaponEquipInventory[0].getFlash(),
                                player.weaponEquipInventory[0].getFirepoint().transform.position,
                                   Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[0].getAccuracy();
                            for (int k = 0; k < player.weaponEquipInventory[0].getBurstCount(); k++)
                            {
                                GameObject bullet = Instantiate(player.weaponEquipInventory[0].getBullet(),
                                    player.weaponEquipInventory[0].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                                BulletScript bScript = bullet.GetComponent<BulletScript>();
                                bScript.damage = player.weaponEquipInventory[0].getDamage();
                                bScript.headshotDamage = player.weaponEquipInventory[0].getHeadshotDamage();
                                bScript.velocity = player.weaponEquipInventory[0].getBulletVelocity();
                                bScript.bulletLife = player.weaponEquipInventory[0].getBulletLife();
                                lastShot = Time.time;
                            }
                            Destroy(flash, player.weaponEquipInventory[0].getFlashTime());
                            player.weaponEquipInventory[0].setCurrentMagAmmo(
                                player.weaponEquipInventory[0].getCurrentMagAmmo() - 1);
                            Debug.Log(player.weaponEquipInventory[0].getCurrentMagAmmo());
                            if (player.weaponEquipInventory[0].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }
                        }
                    }
                    else { Debug.Log("No weapon type"); }
                }
            }

            if (player.switchCount == 2 && player.weaponEquipInventory.Count > 1)
            {
                if (player.weaponEquipInventory[1].getCurrentMagAmmo() > 0)
                {
                    float fireRate = player.weaponEquipInventory[1].getFireRate();
                    if (player.weaponEquipInventory[1].getWeaponType().Equals("ar"))
                    {
                        if (Time.time > fireRate + lastShot)
                        {
                            GameObject flash = Instantiate(player.weaponEquipInventory[1].getFlash(),
                                player.weaponEquipInventory[1].getFirepoint().transform.position,
                                Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[1].getAccuracy();
                            GameObject bullet = Instantiate(player.weaponEquipInventory[1].getBullet(),
                                player.weaponEquipInventory[1].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                            BulletScript bScript = bullet.GetComponent<BulletScript>();
                            bScript.damage = player.weaponEquipInventory[1].getDamage();
                            bScript.headshotDamage = player.weaponEquipInventory[1].getHeadshotDamage();
                            bScript.velocity = player.weaponEquipInventory[1].getBulletVelocity();
                            bScript.bulletLife = player.weaponEquipInventory[1].getBulletLife();
                            lastShot = Time.time;
                            Destroy(flash, player.weaponEquipInventory[1].getFlashTime());
                            player.weaponEquipInventory[1].setCurrentMagAmmo(
                                    player.weaponEquipInventory[1].getCurrentMagAmmo() - 1);
                            if (player.weaponEquipInventory[1].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }
                        }
                    }
                    else if (player.weaponEquipInventory[1].getWeaponType().Equals("handgun"))
                    {
                        if (Time.time > fireRate + lastShot && readyToFire)
                        {
                            readyToFire = false;
                            GameObject flash = Instantiate(player.weaponEquipInventory[1].getFlash(),
                                player.weaponEquipInventory[1].getFirepoint().transform.position,
                                   Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[1].getAccuracy();
                            GameObject bullet = Instantiate(player.weaponEquipInventory[1].getBullet(),
                                player.weaponEquipInventory[1].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                            BulletScript bScript = bullet.GetComponent<BulletScript>();
                            bScript.damage = player.weaponEquipInventory[1].getDamage();
                            bScript.headshotDamage = player.weaponEquipInventory[1].getHeadshotDamage();
                            bScript.velocity = player.weaponEquipInventory[1].getBulletVelocity();
                            bScript.bulletLife = player.weaponEquipInventory[1].getBulletLife();
                            lastShot = Time.time;
                            Destroy(flash, player.weaponEquipInventory[1].getFlashTime());
                            player.weaponEquipInventory[1].setCurrentMagAmmo(
                                    player.weaponEquipInventory[1].getCurrentMagAmmo() - 1);
                            if (player.weaponEquipInventory[1].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }
                        }
                    }
                    else if (player.weaponEquipInventory[1].getWeaponType().Equals("shotgun"))
                    {
                        if (Time.time > fireRate + lastShot && readyToFire
                            && player.weaponEquipInventory[1].getCurrentMagAmmo() > 0 && !reloading)
                        {
                            readyToFire = false;
                            GameObject flash = Instantiate(player.weaponEquipInventory[1].getFlash(),
                                player.weaponEquipInventory[1].getFirepoint().transform.position,
                                   Quaternion.Euler(0f, 0f, rotZ));
                            float accuracy = player.weaponEquipInventory[1].getAccuracy();
                            for (int k = 0; k < player.weaponEquipInventory[1].getBurstCount(); k++)
                            {
                                GameObject bullet = Instantiate(player.weaponEquipInventory[1].getBullet(),
                                    player.weaponEquipInventory[1].getFirepoint().transform.position,
                                                       Quaternion.Euler(0f, 0f,
                                                       (rotZ + Random.Range((-30 + accuracy), (30 - accuracy)))));
                                BulletScript bScript = bullet.GetComponent<BulletScript>();
                                bScript.damage = player.weaponEquipInventory[1].getDamage();
                                bScript.headshotDamage = player.weaponEquipInventory[1].getHeadshotDamage();
                                bScript.velocity = player.weaponEquipInventory[1].getBulletVelocity();
                                bScript.bulletLife = player.weaponEquipInventory[1].getBulletLife();
                                lastShot = Time.time;
                            }
                            Destroy(flash, player.weaponEquipInventory[1].getFlashTime());
                            player.weaponEquipInventory[1].setCurrentMagAmmo(
                                    player.weaponEquipInventory[1].getCurrentMagAmmo() - 1);
                            if (player.weaponEquipInventory[1].getCurrentMagAmmo() == 0)
                            {
                                player.startTime = Time.time;
                            }

                        }
                    }
                    else { Debug.Log("No weapon type"); }
                }  
            }
        }
    }
}
