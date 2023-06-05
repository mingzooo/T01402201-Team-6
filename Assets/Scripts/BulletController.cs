using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 1f;
    private Vector3 direction;

    public void Shoot(float dir)
    {
        this.direction = new Vector3(dir*bulletSpeed,0,0);
        Invoke("DestroyBullet", 5f);
    }
    public void DestroyBullet()
    {
        ObjectPooling.ReturnObjectToQueue(gameObject);
    }
    void FixedUpdate()
    {
        transform.Translate(direction);
    }
}