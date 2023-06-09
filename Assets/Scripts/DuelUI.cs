using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelUI : MonoBehaviour
{
    Slider sli;
    Duel duel;
    // Start is called before the first frame update
    void Start()
    {
        duel = GameObject.Find("Boss").GetComponent<Duel>();
        sli = GetComponent<Slider>();
        sli.maxValue = duel.duelTime;
        sli.value = duel.duelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (sli.value > 0.0f)
        {
            // �ð��� ������ ��ŭ slider Value ������ �մϴ�.
            sli.value = duel.curTime;
        }
        else
        {
            Debug.Log("Time is Zero.");
        }
    }
}
