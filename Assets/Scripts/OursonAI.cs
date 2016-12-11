using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonAI : MonoBehaviour {

    public int life;
    public bool direction;
    public float speed;
    public float dieTime;

    private float dieTimeLeft;
    private bool dead = false;
    private Animator anim;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update () {
        if (!dead)
        {
            float dir = direction ? speed : -speed;
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
        }
        else
        {
            dieTimeLeft -= Time.deltaTime;
            if (dieTimeLeft <= 0)
            {
                Destroy(transform.gameObject);
                return;
            }


        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }

    public void HoleDetected()
    {
        direction = !direction;
    }

    public void WallDetected()
    {
        direction = !direction;
    }

    public void TakeDamages(int damages)
    {
        if (life <= 0)
            return ;
        life -= damages;
        if (life <= 0)
        {
            dead = true;
            dieTimeLeft = dieTime;
            anim.SetBool("dying", true);
        }
    }
}
