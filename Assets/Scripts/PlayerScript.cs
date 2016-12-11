using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 1f;
    public float jumpPower = 10f;
    public Camera cam;
    public GameObject bullet;
    public float fireCoolDown;

    private float fireCdleft = 0;
    private Animator anim;
    private Rigidbody2D rb;
    private bool canJump = true;
    private List<Collider2D> grounds;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        grounds = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounds.Add(collision);
            canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounds.Remove(collision);
        }
        if (grounds.Count == 0)
            canJump = false;
    }

    // Update is called once per frame
    void Update () {
        
        float dir = Input.GetAxis("Horizontal");
        rb.velocity += new Vector2(dir, 0) * speed;
        if (dir < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("Walking", rb.velocity.x >= 0.1f || rb.velocity.x <= -0.1f);

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity += new Vector2(rb.velocity.x, jumpPower);
            canJump = false;
        }
        if (cam != null)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }

        if (fireCdleft > 0)
            fireCdleft -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCdleft <= 0)
        {
            fireCdleft = fireCoolDown;
            if (transform.localScale.x >= 0)
                Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.left));
            else
                Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.right));
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }
}
