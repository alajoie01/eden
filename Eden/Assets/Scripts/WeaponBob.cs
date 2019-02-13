using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour {

    float bobTime = 1f;
    float lastBob = 0f;
    public int direction = -1;
    float bobSpeed = 0.04f;
	
	// Update is called once per frame
	void Update () {

        transform.Translate(new Vector3(0, bobSpeed, 0) * direction);

        if (Time.time > bobTime + lastBob)
        {
            direction *= -1;
            lastBob = Time.time;
        }
    }
}
