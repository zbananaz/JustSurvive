using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float radius = 7f;
    public Vector3 dir;
    public GameObject target;

    public GameObject bullet;

    public float fireRate = 0.5f;
    public float timeToShot = 0.5f;

    //Tìm target và bắn
    private void Update()
    //{
    //    SetTarget();

    //    timeToShot += Time.deltaTime;

    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7f);

    //    if (hit.collider != null)
    //    {
    //        if (hit.collider.CompareTag("Environment"))
    //        {
    //            Debug.Log("Enemy is behind a wall");
    //        }
    //        else
    //        {
    //            if (timeToShot >= fireRate && target != null)
    //            {
    //                Shoot();
    //                timeToShot = 0;
    //            }
    //        }
    //    }
    //}

    {
        SetTarget();

        timeToShot += Time.deltaTime;

        if (target != null)
        {
            dir = target.transform.position - transform.position;
            dir = dir.normalized;

            // Kiểm tra xem enemy có nằm trong tầm nhìn không
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 7f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Environment"))
                {
                    Debug.Log("Enemy is behind a wall");
                    return;
                }
                else
                {
                    if (timeToShot >= fireRate)
                    {
                        Shoot();
                        timeToShot = 0;
                    }
                }
            }
        }
    }

    //Tìm target
    public void SetTarget()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));
       

        if(collider2Ds.Length == 0)
        {
            target = null;
            return;
        }

        foreach(Collider2D c in collider2Ds)
        {
            if ( c.gameObject.CompareTag("Enemy"))
            {
                target = c.gameObject;
            }
        }
    }


    public void Shoot()
    {
        
        dir = target.transform.position - transform.position;
        dir = dir.normalized;

        bullet = ObjectPooling.instance.GetObjectFromPool("Bullet");
        bullet.transform.position = transform.position;
        bullet.SetActive(true);

        // truyen luc cho vien dan
        bullet.GetComponent<Bullet>().SetSpeed(dir, 20f);

        //xoay vien dan
        bullet.transform.Rotate(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
    }
}

// xử lý biến mất đạn
//Tạo list lưu enemy trong tầm đánh, thằng nào ở ngoài thì cho ra.

