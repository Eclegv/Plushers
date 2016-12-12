using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonAI : MonoBehaviour {

    public int life;
    public bool direction;
    public float speed;
    public float dieTime;
    public int attackPower;
    public GameObject attack;

    private float dieTimeLeft;
    private bool dead = false;
    private bool hole, wall;
    private Animator anim;
    private Rigidbody2D rb;
    private float blinkingLeft;
    private SpriteRenderer spriterenderer;
    private Transform target = null;
    private float attackLeft = 0f;
    private bool lastframeattack = false;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update () {
        if (!dead)
        {
            if (attackLeft > 0)
            {
                attackLeft -= Time.deltaTime;
                anim.SetBool("Attacking", attackLeft > 0.2f);
                lastframeattack = true;
            }
            else
            {
                anim.SetBool("Attacking", false);
                if (lastframeattack)
                    lastframeattack = false;
                else
                {
                    if ((hole || wall) && target == null)
                        direction = !direction;
                    if (target != null)
                    {
                        float xdif = (target.position - transform.position).x;
                        bool shouldGo = Mathf.Abs(xdif) > 0.7f && !hole && !wall;
                        if (xdif < 0)
                        {
                            direction = false;
                        }
                        else if (xdif > 0)
                        {
                            direction = true;
                        }

                        float dir = direction ? speed : -speed;
                        if (shouldGo)
                            rb.velocity += new Vector2(dir, 0) * speed;
                        if (dir < 0)
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        else if (dir > 0)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        if (Mathf.Abs(xdif) <= 0.7f)
                        {
                            Instantiate(attack, transform.TransformPoint(-0.6f, 0, 0), Quaternion.identity, transform).GetComponent<OursonAttackFx>().damages = attackPower;
                            attackLeft = 2f;
                            anim.SetBool("Attacking", true);
                        }
                    }
                    else if (target == null)
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
                    }
                }
            }
            anim.SetBool("Walking", rb.velocity.x >= 0.1f || rb.velocity.x <= -0.1f);

            if (blinkingLeft > 0)
            {
                blinkingLeft -= Time.deltaTime;
                if (((int)(blinkingLeft * 100)) % 2 == 0)
                    spriterenderer.color = Color.red;
                else
                    spriterenderer.color = Color.white;
            }
            else
            {
                spriterenderer.color = Color.white;
            }
        }
        else
        {
            dieTimeLeft -= Time.deltaTime;
            if (dieTimeLeft <= 0)
            {
                GameObject.Find("Scripts").GetComponents<PlayerPlatformManager>()[0].AddOurson(1);
                Destroy(transform.gameObject);
                return;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }

    public void SetHole(bool b)
    {
        hole = b;
    }

    public void SetWall(bool b)
    {
        wall = b;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void CancelTarget()
    {
        target = null;
    }

    public void TakeDamages(Hit hit)
    {
        rb.velocity += new Vector2(hit.Direction.x, hit.Direction.y);
        if (life <= 0)
            return ;
        life -= hit.Damages;
        if (life <= 0)
        {
            dead = true;
            dieTimeLeft = dieTime;
            anim.SetBool("dying", true);
        }
        else
        {
            blinkingLeft = 0.5f;
        }
    }
}
