using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // AudioSource ������Ʈ�� ������ ����
    private AudioSource audioSource;

    [SerializeField]
    private float bulletSpeed = 5f;
    private Vector3 direction;
    private GameObject owner;
    private int damage = 1;
    private Rigidbody2D bulletRb;


    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        // AudioSource ������Ʈ ����, ���� �Ҵ�
        audioSource = GetComponent<AudioSource>();
    }

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
        // Play gun sound
        audioSource.Play();
    }
    public void DestroyBullet()
    {
        ObjectPooling.ReturnObjectToQueue(gameObject);
    }


    private void Update()
    {
        if (transform.position.x > 500f || transform.position.x < -100f) DestroyBullet();
    }

    void FixedUpdate()
    {
        bulletRb.velocity = direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Damage(owner, collider.gameObject, damage);
    }
    private void Damage(GameObject attacker, GameObject target, int damage)
    {
        if (attacker == null) return;
        if (attacker.tag == "Enemy" || attacker.tag == "Boss")
        {
            if (target.tag == "Player" && !target.GetComponent<PlayerController>().CheckRoll())
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
            if (target.tag == "Boss" && !target.GetComponent<BossController>().CheckRoll())
            {
                target.GetComponent<BossController>().BossCurHp -= damage;
                DestroyBullet();
            }
        }
    }
}