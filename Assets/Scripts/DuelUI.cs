//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class DuelUI : MonoBehaviour
//{
//    Slider sli;
//    Duel duel;
//    // Start is called before the first frame update
//    void Start()
//    {
//        duel = GameObject.Find("Boss").GetComponent<Duel>();
//        sli = GetComponent<Slider>();
//        sli.maxValue = duel.duelTime;
//        sli.value = duel.duelTime;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (sli.value > 0.0f)
//        {
//            // 시간이 변경한 만큼 slider Value 변경을 합니다.
//            sli.value = duel.curTime;
//        }
//        else
//        {
//            Debug.Log("Time is Zero.");
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class DuelUI : MonoBehaviour
{
    Slider slTimer;
    float fSliderBarTime;
    void Start()
    {
        slTimer = GetComponent<Slider>();
    }

    void Update()
    {
        if (slTimer.value > 0.0f)
        {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
            slTimer.value -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time is Zero.");
        }
    }
}
