using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Duel duel;

    private int phase = 1;

    private float shootCd = 1f;
    private float shootCurCd = 0f;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isRolling = false;

    public Transform playerTransform;

    private void Awake()
    {
        duel = GetComponent<Duel>();
    }
    void Start()
    {
        // duel.startDuel = true;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        // Jump();
        // Roll();
    }

    void FixedUpdate()
    {

    }

    void Shoot()
    {
        if ((playerTransform.position.x > transform.position.x && facingRight) || (playerTransform.position.x < transform.position.x && !facingRight))
        {
            if (!isRolling && shootCurCd <= 0f)
            {
                shootCurCd = shootCd;
                GameObject bullet = ObjectPooling.GetObject();
                float dir = 1f;
                if (!facingRight)
                    dir *= -1f;
                Vector3 spawnPos = new Vector3(transform.position.x + dir * 0.7f, transform.position.y + 1.29f, transform.position.z);
                bullet.transform.position = spawnPos;
                bullet.GetComponent<BulletController>().SetOwner(gameObject);
                bullet.GetComponent<BulletController>().Shoot(dir);
            }
            else if (shootCurCd > 0f)
            {
                shootCurCd -= Time.deltaTime;
            }   
        }
    }
}
