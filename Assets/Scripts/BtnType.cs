using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Vector3 buttonScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
    }

    public void onBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                Debug.Log("게임 시작");
                break;
            case BTNType.Tutorial:
                Debug.Log("게임 방법");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }
}
