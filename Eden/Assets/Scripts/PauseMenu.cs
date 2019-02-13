using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool isPaused = false;

    public GameObject[] weapons;

    public GameObject gun;
    public GameObject player;
    public GameObject playerArm;

	// Use this for initialization
	void Start () {

        PauseUI.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        weapons = GameObject.FindGameObjectsWithTag("Weapon Pickups");
        player = GameObject.Find("Player");
        playerArm = player.transform.GetChild(2).gameObject;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
            foreach (GameObject weapon in weapons)
            {
                gun = weapon.transform.GetChild(1).gameObject;
                gun.GetComponent<WeaponBob>().enabled = false;
                player.GetComponent<Player>().enabled = false;
                playerArm.GetComponent<PlayerArm>().enabled = false;
            }
        }

        if (!isPaused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            foreach (GameObject weapon in weapons)
            {
                gun = weapon.transform.GetChild(1).gameObject;
                gun.GetComponent<WeaponBob>().enabled = true;
                player.GetComponent<Player>().enabled = true;
                playerArm.GetComponent<PlayerArm>().enabled = true;
            }
        }
    }

    public void resume()
    {
        isPaused = false;
    }

    public void quit()
    {
        Application.Quit();
    }
}
