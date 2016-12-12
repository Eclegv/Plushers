using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float speed = 1f;
    public float jumpPower = 10f;
    public float damageCooldown = 2f;
    public Camera cam;
    public GameObject bullet;
    public float fireCoolDown;
    public List<GameObject> corpses;

    private float fireCdleft = 0;
    private float moveAfterFire = 0;
    private float damageCdLeft = 0;
    private Animator anim;
    private Rigidbody2D rb;
    private bool canJump = true;
    private bool dead = false;
    private List<Collider2D> grounds;
    private GameObject scripts;
    private PlayerPlatformManager platforms;
    private PlayerLifeManager life;
    private SpriteRenderer spriterenderer;
    private bool dancing = false;

    private Color transparent;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        grounds = new List<Collider2D>();
        scripts = GameObject.Find("Scripts");
        platforms = scripts.GetComponent<PlayerPlatformManager>();
        life = scripts.GetComponent<PlayerLifeManager>();
        spriterenderer = GetComponent<SpriteRenderer>();
        transparent = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Finish"))
        {
            dancing = true;
            StartCoroutine(End());
            return;
        }
        if (collision.gameObject.name.Contains("Ground") || collision.gameObject.name.Contains("Mort"))
        {
            grounds.Add(collision);
            canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground") || collision.gameObject.name.Contains("Mort"))
        {
            grounds.Remove(collision);
        }
        if (grounds.Count == 0)
            canJump = false;
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update () {
        if (cam != null)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }
        if (!dead)
        {
            if (dancing)
            {
                anim.SetBool("Dancing", true);
            }
            else
            {
                if (damageCdLeft > 0)
                {
                    damageCdLeft -= Time.deltaTime;
                    if (((int)(damageCdLeft * 100)) % 2 == 0)
                        spriterenderer.color = transparent;
                    else
                        spriterenderer.color = Color.white;
                }
                else
                    spriterenderer.color = Color.white;

                if (moveAfterFire > 0)
                {
                    moveAfterFire -= Time.deltaTime;
                    if (moveAfterFire <= 0)
                    {
                        if (transform.localScale.x < 0)
                            Instantiate(bullet, transform.position - new Vector3(0, 0.1f, 0), Quaternion.FromToRotation(Vector3.up, Vector3.left));
                        else
                            Instantiate(bullet, transform.position - new Vector3(0, 0.1f, 0), Quaternion.FromToRotation(Vector3.up, Vector3.right));
                    }
                }
                else
                {
                    anim.SetBool("Shooting", false);
                    float dir = Input.GetAxis("Horizontal");
                    rb.velocity += new Vector2(dir, 0) * speed;
                    if (dir < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (dir > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    anim.SetBool("Walking", rb.velocity.x >= 0.1f || rb.velocity.x <= -0.1f);

                    if (Input.GetButtonDown("Jump") && canJump)
                    {
                        rb.velocity += new Vector2(rb.velocity.x, jumpPower);
                        canJump = false;
                    }

                    if (Input.GetButtonDown("Fire3"))
                    {
                        if (platforms.GetCount() > 0)
                        {
                            Instantiate(corpses[Random.Range(0, corpses.Count)], transform.position - new Vector3(0, 0.50f, 0), Quaternion.identity);
                            platforms.RemoveOurson(1);
                        }
                    }

                    if (fireCdleft > 0)
                        fireCdleft -= Time.deltaTime;
                    if (Input.GetButton("Fire1") && fireCdleft <= 0)
                    {
                        fireCdleft = fireCoolDown;
                        /*if (transform.localScale.x < 0)
                            Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.left));
                        else
                            Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.right));*/
                        moveAfterFire = 0.2f;
                        anim.SetBool("Shooting", true);
                    }

                    anim.SetBool("Jumping", rb.velocity.y > 0.01f);
                    anim.SetBool("Falling", rb.velocity.y < -0.01f);
                }
            }
        }
        else
        {
            spriterenderer.color = Color.white;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x / 1.2f, rb.velocity.y);
    }

    public void TakeDamages(int damages)
    {
        if (damageCdLeft > 0)
            return ;
        life.TakeDamages(damages);
        if (life.life == 0)
        {
            dead = true;
            anim.SetBool("Dying", true);
        }
        damageCdLeft = 2f;

    }
}
