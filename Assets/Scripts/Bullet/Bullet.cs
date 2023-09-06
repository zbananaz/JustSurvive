using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 10;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    public void SetSpeed(Vector3 dir, float spd)
    {
        rb.velocity = dir * spd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

            gameObject.transform.rotation = Quaternion.identity;
            collision.gameObject.GetComponent<BatEnemy>().TakeDamage(dmg);
        }
        else if (collision.gameObject.CompareTag("Environment"))
        {
            gameObject.SetActive(false);
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
