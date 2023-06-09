using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private float shootCd = 0.5f;

    private Rigidbody2D playerRb; // 플레이어의 리지드바디
    private Animator playerAnim;
    private AudioSource audiosource;

    private float shootCurCd = 0f;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isRolling = false;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
    }

  // Start is called before the first frame update
  void Start()
  {
    playerRb = GetComponent<Rigidbody2D>();
    playerAnim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
        Shoot();
        Jump();

        if (Input.GetKeyDown(KeyCode.C) && !isRolling && !isJumping)
        {
            StartCoroutine(Roll());
        }
  }

  private void FixedUpdate()
  {
    if (!isRolling)
    {
      float move = Input.GetAxisRaw("Horizontal");
      playerRb.velocity = new Vector2(move * moveSpeed, playerRb.velocity.y);

      if (move == 0f)
        playerAnim.SetBool("Running", false);
      else
        playerAnim.SetBool("Running", true);

      if (move > 0 && !facingRight)
      {
        Flip();
      }
      else if (move < 0 && facingRight)
      {
        Flip();
      }
    }
  }

  private void Flip()
  {
    facingRight = !facingRight;
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
  }
  private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isRolling && shootCurCd <= 0f)
        {
            shootCurCd = shootCd;
            audiosource.Play();
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
  private void Jump()
  {
    if (Input.GetKeyDown(KeyCode.X) && !isJumping && !isRolling)
        {
            playerRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
  }
  private IEnumerator Roll()
  {
    isRolling = true;
    InitAnim();
    playerAnim.SetBool("Rolling", true);
    if (facingRight)
    {
      playerRb.velocity = new Vector2(moveSpeed, playerRb.velocity.y);
    }
    else
    {
      playerRb.velocity = new Vector2(-moveSpeed, playerRb.velocity.y);
    }
    yield return new WaitForSeconds(0.5f);
    playerAnim.SetBool("Rolling", false);
    isRolling = false;
  }
  private void InitAnim()
    {
        playerAnim.SetBool("Running", false);
        playerAnim.SetBool("Rolling", false);
    }

  
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJumping = false;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
      isJumping = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    // 포탈에 닿으면
    if (collision.gameObject.CompareTag("Finish"))
    {
      // 모든 적을 물리치면 다음 스테이지로
      if (GameManager.Instance.CheckAllEnemiesDefeated())
      {
        GameManager.Instance.LoadNextStage();
      }
      // 적이 남아있으면
      else
      {
        Debug.Log("Cannot proceed to the next stage. Defeat all enemies first!");
      }
    }
  }
}
