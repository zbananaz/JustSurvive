using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    Vector2 dir;

    protected int maxHealth;
    protected int damage;
    protected int experience;
    protected int moveSpeed;

    protected int currentHealth;

    protected virtual void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        dir = (target.transform.position - transform.position).normalized;
        //agent.SetDestination(target.transform.position);
    }
    
    protected virtual void FixedUpdate()
    {
        rb.velocity = moveSpeed * Time.fixedDeltaTime * dir;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //Logic khi quai vat chet
        gameObject.SetActive(false);
    }
}
