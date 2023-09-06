using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        //Bo sung logic rieng (neu can).
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        //Bo sung logic rieng (neu can).
    }

    protected override void Die()
    {
        base.Die();
    }

    
}
