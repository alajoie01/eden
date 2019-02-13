using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour {

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            transform.parent.GetComponent<BuzzerPatrol>().CollisionDetected(collision.gameObject);
        }
    }
}
