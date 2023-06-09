using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Duel : MonoBehaviour
{
    [SerializeField]
    private int duelCount = 5;
    
    public float duelTime = 5f;
    public float curTime = 5f;

    public bool startDuel = false;
    public bool isDuel = false;

    KeyCode code = KeyCode.Z;
    public TextMeshProUGUI keyText;

    void Start()
    {
        keyText = GameObject.Find("BossKeyText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (startDuel)
        {
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
            else if(CheckTime() && duelCount > 0)
            {
                int isSuccess = CheckCode(code);
                if(isSuccess == 1)
                {
                    //����
                    isDuel = false;
                    duelCount--;
                }
                else if(isSuccess == -1)
                {
                    // ���ӿ���
                    Debug.Log("fail");
                    startDuel = false;
                    keyText.text = "FAIL";
                }
            }
            else if(duelCount <= 0)
            {
                //���
                Debug.Log("success");
                startDuel = false;
                keyText.text = "SUCCESS!";
            }
            else
            {
                // ���ӿ���
                Debug.Log("time fail");
                startDuel = false;
                keyText.text = "FAIL";
            }
        }
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
        keyText.text = "PRESS " + code.ToString();
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
