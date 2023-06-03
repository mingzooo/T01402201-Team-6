using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private bool facingRight = true;
    private bool isJumping = false;
    private bool isRolling = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isRolling)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.X) && !isJumping && !isRolling)
        {
            Jump();
        }

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
        GameObject bullet = ObjectPooling.GetObject();
        float dir = 1f;
        if (!facingRight)
            dir *= -1f;
        Vector3 spawnPos = new Vector3(transform.position.x + dir * 0.7f, transform.position.y + 1.29f, transform.position.z);
        bullet.transform.position = spawnPos;
        bullet.GetComponent<BulletController>().Shoot(dir);
    }
    private void Jump()
    {
        playerRb.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
    }
    private IEnumerator Roll()
    {
        isRolling = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJumping = false;
        else if (collision.gameObject.tag == "Bullet")
            GameManager.Instance.PlayerDamaged(1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJumping = true;
    }

}
