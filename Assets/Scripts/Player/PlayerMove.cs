using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 movementVector;
    [SerializeField] private float speed = 100f;
    Animator animator;

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

        movementVector *= speed;

        rb.velocity = movementVector * Time.fixedDeltaTime;

        Animate(movementVector.magnitude > .1f);



    }

    private void Animate(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger("moveLeft");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("moveRight");
        }
    }
}
