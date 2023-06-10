using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 1f;
    private Vector3 direction;
    private GameObject owner;
    private int damage = 1;

    public void SetOwner(GameObject gameobject)
    {
        owner = gameobject;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(owner, collision.gameObject, damage);
    }
    private void Damage(GameObject attacker, GameObject target, int damage)
    {
        if (attacker.tag == "Enemy" || attacker.tag == "Boss")
        {
            if (target.tag == "Player")
            {
                GameManager.Instance.SetPlayerHp(damage);
                DestroyBullet();
            }
        }
        else if (attacker.tag == "Player")
        {
            if (target.tag == "Enemy")
            {
                target.GetComponent<EnemyController>().enemyHealth -= damage;
                DestroyBullet();
            }
            if (target.tag == "Boss")
            {
                target.GetComponent<BossController>().BossCurHp -= damage;
                DestroyBullet();
            }
        }
    }
}