using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    Vector2 dir;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dir = (target.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * Time.fixedDeltaTime * dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject == target)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
