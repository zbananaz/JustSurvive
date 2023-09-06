using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    Vector2 dir;

    public int maxHealth = 10;
    public int damage = 5;
    public int experience = 10;
    public int moveSpeed = 100;

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
