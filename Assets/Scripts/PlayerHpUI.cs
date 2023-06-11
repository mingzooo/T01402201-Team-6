using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    //¸ñ¼û °¹¼ö
    public GameObject[] life;
    // Start is called before the first frame update
    void Update()
    {
        UpdateLifeIcon();
    }
    public void UpdateLifeIcon()
    {
        int php = GameManager.Instance.playerHp;
        for (int index = 0; index < 5; index++)
        {
            life[index].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < php; index++)
        {
            life[index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
