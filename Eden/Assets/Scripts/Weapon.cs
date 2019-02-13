using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject {

    public string realName;
    public string weaponName;
    public GameObject flash = null;
    public GameObject bullet = null;

    public int damage;
    public int headshotDamage;
    public int magAmmo;
    public int totalAmmo;
    public int bulletVelocity;

    public float fireRate;
    public float flashTime;
    public float accuracy;
    public float bulletLife;
    public float reloadSpeed;

    public string weaponType;
    public int burstCount;
    public int weaponTier;
    public int currentMagAmmo;
    public int currentTotalAmmo;

}
