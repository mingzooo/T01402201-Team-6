using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Duel duel;

    [SerializeField]
    public int enemyHealth = 30;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private float shootCd = 1f;

    private int phase = 1;

    private float shootCurCd = 0f;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isRolling = false;

    public Transform playerTransform;

    private Rigidbody2D bossRb;
    private Animator bossAnim;

    private void Awake()
    {
        duel = GetComponent<Duel>();
    }
    void Start()
    {
        // duel.startDuel = true;
        bossRb = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Jump();
        if (Input.GetKeyDown(KeyCode.R) && !isRolling && !isJumping)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {

    }

    void Move()
    {
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isJumping && !isRolling)
        {
            bossRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
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
    private IEnumerator Roll()
    {
        isRolling = true;
        InitAnim();
        bossAnim.SetBool("Rolling", true);
        if (facingRight)
        {
            bossRb.velocity = new Vector2(moveSpeed, bossRb.velocity.y);
        }
        else
        {
            bossRb.velocity = new Vector2(-moveSpeed, bossRb.velocity.y);
        }
        yield return new WaitForSeconds(0.5f);
        bossAnim.SetBool("Rolling", false);
        isRolling = false;
    }
    private void InitAnim()
    {
        bossAnim.SetBool("Running", false);
        bossAnim.SetBool("Rolling", false);
    }
}
