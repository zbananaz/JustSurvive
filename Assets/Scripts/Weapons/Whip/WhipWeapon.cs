using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField]
    private float timeToAttack = 1.5f;
    private float timer;

    [SerializeField]
    private int whipDamage = 10;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] private Vector2 whipAttackSize = new(4f,2f);

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = timeToAttack;
        }
    }

    private void Attack()
    {
        if(playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(collider2Ds);
        }
        else
        {
            leftWhipObject.SetActive(true);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(collider2Ds);
        }
    }

    private void ApplyDamage(Collider2D[] collider2Ds)
    {
        for (int i = 0; i< collider2Ds.Length; i++)
        {
            Debug.Log("hit................." + collider2Ds[i].gameObject.name + ".......................");
            if (collider2Ds[i].CompareTag("Enemy"))
            {
                collider2Ds[i].GetComponent<BatEnemy>().TakeDamage(whipDamage);
            }
        }
    }
}