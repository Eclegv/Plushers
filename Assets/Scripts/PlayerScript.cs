using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float dir = Input.GetAxis("Horizontal");
        rb.velocity += new Vector2(dir, 0) * speed;
	}
}
