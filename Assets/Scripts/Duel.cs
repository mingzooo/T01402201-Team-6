using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel : MonoBehaviour
{
    [SerializeField]
    private int duelCount = 5;

    public float duelTime = 5f;
    public float curTime = 5f;

    public bool startDuel = false;
    public bool isDuel = false;

    KeyCode code = KeyCode.Z;

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
                    //성공
                    isDuel = false;
                    duelCount--;
                }
                else if(isSuccess == -1)
                {
                    // 게임오버
                    Debug.Log("fail");
                    startDuel = false;
                }
            }
            else if(duelCount <= 0)
            {
                //통과
                Debug.Log("success");
                startDuel = false;
            }
            else
            {
                // 게임오버
                Debug.Log("time fail");
                startDuel = false;
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
        //UI로 입력해야하는 코드와 시간 보여줌
        Debug.Log(code);
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
