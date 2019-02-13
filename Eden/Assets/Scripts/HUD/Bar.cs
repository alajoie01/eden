using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float shield;
    public float maxShield;

    GameObject player;
    Player playerScript;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image shieldBar;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerScript.health = health;
        playerScript.shield = shield;
        playerScript.maxHealth = maxHealth;
        playerScript.maxShield = maxShield;
        Manipulate();
    }

    private void Manipulate()
    {
        healthBar.fillAmount = percentConversion(health, maxHealth);
        shieldBar.fillAmount = percentConversion(shield, maxShield);
    }

    private float percentConversion(float current, float max)
    {
        return (current / max);
    }
}
