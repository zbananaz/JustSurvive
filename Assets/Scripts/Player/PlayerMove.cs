using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVertitalVector;

    [HideInInspector]
    public Vector3 movementVector;

    [SerializeField]
    private float speed = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        movementVector = new Vector3();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if (movementVector.x !=0)
        {
            lastHorizontalVector = movementVector.x;
        }
        if (movementVector.y != 0)
        {
            lastVertitalVector = movementVector.y;
        }

        movementVector *= speed;

        rb.velocity = movementVector * Time.fixedDeltaTime;

        Animate(movementVector.magnitude > .1f);
    }

    private void Animate(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);

        if(rb.velocity.x > 0)
        {
            animator.SetTrigger("moveRight");
        }
        else if( rb.velocity.x < 0)
        {
            animator.SetTrigger("moveLeft");
        }


        //if (Input.GetKeyDown(KeyCode.LeftArrow) )
        //{
        //    animator.SetTrigger("moveLeft");
        //}else
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    animator.SetTrigger("moveRight");
        //    }
    }
}
