using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private Slider BossHpBar;

    Duel duel;

    [SerializeField]
    public float BossMaxHp = 30;
    [SerializeField]
    public float BossCurHp = 30;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float rollSpeed = 5f;
    [SerializeField]
    private float jumpPower = 7f;
    [SerializeField]
    private float shootCd = 1f;
    [SerializeField]
    private float jumpCd = 4f;
    [SerializeField]
    private float rollCd = 3f;

    private int phase = 1;

    private float shootCurCd = 0f;
    private float jumpCurCd = 0f;
    private float rollCurCd = 0f;
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
        BossHpBar.maxValue = (float)BossMaxHp;
        BossHpBar.value = (float)BossCurHp;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (!isRolling)
        {
            Move();
        }

        Jump();

        if (!isRolling && !isJumping)
        {
            if (rollCurCd <= 0f)
            {
                rollCurCd = rollCd;
                StartCoroutine(Roll());
            }
            else if (rollCurCd > 0f)
            {
                rollCurCd -= Time.deltaTime;
            }
        }

        HandleHp();
    }

    void HandleHp()
    {
        BossHpBar.value = Mathf.Lerp(BossHpBar.value, BossCurHp, Time.deltaTime * 10);
    }

    void FixedUpdate()
    {

    }

    public bool CheckRoll()
    {
        return isRolling;
    }
    void Move()
    {
        //move to player
        if (playerTransform.position.x - transform.position.x > 3f)
        {
            bossAnim.SetBool("Running", true);
            bossRb.velocity = new Vector2(moveSpeed, bossRb.velocity.y);
            if(!facingRight)
                Flip();
            facingRight = true;
        }
        else if(playerTransform.position.x - transform.position.x < -3f)
        {
            bossAnim.SetBool("Running", true);
            bossRb.velocity = new Vector2(-moveSpeed, bossRb.velocity.y);
            if(facingRight)
                Flip();
            facingRight = false;
        }
        else{
            bossAnim.SetBool("Running", false);
        }
    }

    void Jump()
    {
        if (!isJumping && !isRolling && jumpCurCd <= 0f)
        {
            jumpCurCd = jumpCd;
            bossRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else if (jumpCurCd > 0f)
        {
            jumpCurCd -= Time.deltaTime;
        }
    }
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Ground")
    //         isJumping = false;
    // }
    
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Ground")
    //     isJumping = true;
    // }
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
            bossRb.velocity = new Vector2(rollSpeed, bossRb.velocity.y);
        }
        else
        {
            bossRb.velocity = new Vector2(-rollSpeed, bossRb.velocity.y);
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

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
