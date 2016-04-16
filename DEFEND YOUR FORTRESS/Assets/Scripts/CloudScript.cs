using UnityEngine;
using System;
using System.Collections;

public class CloudScript : MonoBehaviour {

    int movementSpeed = -2;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        rb.drag = UnityEngine.Random.Range(4, 25);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 walkSpeed = new Vector2(movementSpeed, 0);
        if (Math.Abs(rb.velocity.x) < 3)
        {
            walkSpeed.x = movementSpeed;
        }
        else
        {
            walkSpeed.x = 0;
        }
        rb.AddForce(walkSpeed);
    }
}
