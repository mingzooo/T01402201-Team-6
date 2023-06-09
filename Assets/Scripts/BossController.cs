using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private Slider BossHpBar;

    private float maxHp = 100;
    private float curHp = 100;
    float temp;

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
        BossHpBar.maxValue = (float)maxHp;
        BossHpBar.value = (float)curHp;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        // Jump();
        // Roll();
        Debug.Log(curHp);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curHp > 0){
                curHp -= 10;
            }
            else
            {
                curHp = 0;
            }
        }
        HandleHp();
    }

    void HandleHp()
    {
        BossHpBar.value = Mathf.Lerp(BossHpBar.value, curHp, Time.deltaTime * 10);
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
