using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRb;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

    private bool facingRight = true;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(move * moveSpeed, playerRb.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        playerRb.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJumping = true;
    }

}
