using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class Duel : MonoBehaviour
{
    [SerializeField]
    private int duelCount = 5;
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    public float duelTime = 5f;
    public float curTime = 5f;

    public bool startDuel = false;
    public bool isDuel = false;
    public bool duelReady = false;

    KeyCode code = KeyCode.Z;
    private GameObject player;
    public TextMeshProUGUI keyText;
    public GameObject timeSlider;

    void Start()
    {
        keyText = GameObject.Find("BossKeyText").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (startDuel)
        {
            vcam.Priority = 11;
            GameManager.Instance.Dueling = true;
            player.transform.position = new Vector3(11.5f, 3.85f, 0);
            player.GetComponent<PlayerController>().InitAnim();
            if (!player.GetComponent<PlayerController>().facingRight) player.GetComponent<PlayerController>().Flip();

            transform.position = new Vector3(18.5f, 3.85f, 0);
            GetComponent<BossController>().InitAnim();
            if (GetComponent<BossController>().facingRight) GetComponent<BossController>().Flip();

            if (!duelReady) Invoke("DuelReady", 3f);
            else Duels();
        }
    }
    public void Duels()
    {
        timeSlider.SetActive(true);
        if (!isDuel && duelCount > 0)
        {
            int num = Random.Range(0, 7);
            switch (num)
            {
                case 0:
                    code = KeyCode.Z;
                    break;
                case 1:
                    code = KeyCode.X;
                    break;
                case 2:
                    code = KeyCode.C;
                    break;
                case 3:
                    code = KeyCode.UpArrow;
                    break;
                case 4:
                    code = KeyCode.LeftArrow;
                    break;
                case 5:
                    code = KeyCode.DownArrow;
                    break;
                case 6:
                    code = KeyCode.RightArrow;
                    break;
            }
            isDuel = true;
            ResetTime();
        }
        else if (CheckTime() && duelCount > 0)
        {
            int isSuccess = CheckCode(code);
            if (isSuccess == 1)
            {
                //����
                isDuel = false;
                duelCount--;
            }
            else if (isSuccess == -1)
            {
                // ���ӿ���
                Debug.Log("fail");
                DuelEnd();
                keyText.text = "FAIL";
            }
        }
        else if (duelCount <= 0)
        {
            //���
            Debug.Log("success");
            DuelEnd();
            keyText.text = "SUCCESS!";
        }
        else
        {
            // ���ӿ���
            Debug.Log("time fail");
            DuelEnd();
            keyText.text = "FAIL";
        }
    }
    public void DuelReady()
    {
        duelReady = true;
    }
    public void DuelEnd()
    {
        timeSlider.SetActive(false);
        vcam.Priority = 5;
        duelReady = false;
        ResetTime();
        SetDuelCount(5);
        startDuel = false;
        GameManager.Instance.Dueling = false;
        Invoke("ResetText", 1f);
    }

    public void ResetText()
    {
        keyText.text = "";
    }

    public void ResetTime()
    {
        curTime = duelTime;
    }

    public void SetDuelCount(int value)
    {
        duelCount = value;
    }

    private int CheckCode(KeyCode code)
    {
        //UI�� �Է��ؾ��ϴ� �ڵ�� �ð� ������
        Debug.Log(code);
        keyText.text = code.ToString();
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(code))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        return 0;
    }

    private bool CheckTime()
    {
        if (curTime < 0) return false;
        curTime -= Time.deltaTime;
        return true;
    }

}
