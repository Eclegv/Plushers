using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;
    public Camera cam;

    float horizontalMove = 0f;

    public float speed = 40f;

    bool jump = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetBool("Walking", horizontalMove != 0);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }


    private void FixedUpdate()
    {
        if (cam != null)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
