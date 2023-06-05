using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float recognitionDistance = 5f;
    private bool isRecognized = false;

    private Rigidbody2D enemyRb;
    private Animator enemyAnim;

    private bool facingRight = true;
    public int nextMove;
    public Transform playerTransform;

    private bool canShoot = true;  // 총알 발사 가능 여부
    
    [SerializeField]
    private float shootCooldown = 1f;  // 총알 발사 쿨타임

    [SerializeField]
    private float maxMove = 3f; // 최대 이동 거리
    private Vector2 initialPosition; // 처음 위치 저장

    bool isNearbySpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        initialPosition = transform.position; // 처음 위치 저장

        Invoke("Move", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        enemyRb.velocity = new Vector2(nextMove * moveSpeed, enemyRb.velocity.y);

        isRecognized = Detect();

        if(isRecognized)
        {
            // enemyAnim.SetBool("isShoot", true);
            if (canShoot)  // 총알 발사 가능 상태일 때만 실행
            {
                Shoot();
                StartCoroutine(ShootCooldown());  // 총알 발사 쿨타임 적용
            }
        }
        else
        {
            // enemyAnim.SetBool("isShoot", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Move()
    {
        if(!isRecognized)
        {
            float distanceFromInitial = Mathf.Abs(transform.position.x - initialPosition.x); // 현재 위치와 처음 위치의 거리 계산

            if (distanceFromInitial >= maxMove && isNearbySpawn) // 최대 이동 거리를 초과한 경우 이동 방향 변경하지 않음
            {
                if(initialPosition.x > transform.position.x)
                {
                    nextMove = 1;
                }
                else
                {
                    nextMove = -1;
                }
                isNearbySpawn = false;
            }
            else
            {
                isNearbySpawn = true;
                nextMove = Random.Range(-1, 2);
            }
                if(nextMove == 0)
                {
                    enemyAnim.SetBool("isMove", false);
                }
                else if(nextMove > 0)
                {
                    enemyAnim.SetBool("isMove", true);
                    if (!facingRight)
                    {
                        Flip();
                    }
                }
                else if(nextMove < 0)
                {
                    enemyAnim.SetBool("isMove", true);
                    if (facingRight)
                    {
                        Flip();
                    }
                }
        }
        else
        {
            if (playerTransform.position.x > transform.position.x)
            {
                nextMove = 1;
                if (!facingRight)
                {
                    Flip();
                }
            }
            else
            {
                nextMove = -1;
                if (facingRight)
                {
                    Flip();
                }
            }
            enemyAnim.SetBool("isMove", true);
        }
        float time = Random.Range(1f, 2f);

        Invoke("Move", time);
    }

    void Shoot()
    {
        GameObject bullet = ObjectPooling.GetObject();
        float dir = 1f;
        if (!facingRight)
            dir *= -1f;
        Vector2 spawnPos = new Vector2(transform.position.x + dir, transform.position.y);
        bullet.transform.position = spawnPos;
        bullet.GetComponent<BulletController>().Shoot(dir);
    }

     IEnumerator ShootCooldown()
    {
        canShoot = false;  // 총알 발사 불가능 상태로 변경
        yield return new WaitForSeconds(shootCooldown);  // 쿨다운 시간만큼 대기
        canShoot = true;  // 쿨다운이 끝나면 다시 총알 발사 가능 상태로 변경
    }

    bool Detect()
    {
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            return distance < recognitionDistance;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}