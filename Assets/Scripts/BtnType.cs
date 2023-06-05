using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    public GameObject tutorialScreen; // 튜토리얼 화면을 나타내는 UI 요소
    public GameObject startStory;
    public float fadeInDuration = 0.5f; // 페이드 인에 걸리는 시간

    private CanvasGroup tutorialCanvasGroup; // 튜토리얼 화면의 캔버스 그룹

    private void Start()
    {
        defaultScale = buttonScale.localScale;
        tutorialScreen.SetActive(false);
        startStory.SetActive(false);
        tutorialCanvasGroup = tutorialScreen.GetComponent<CanvasGroup>();
        tutorialCanvasGroup.alpha = 0f; // 초기에 알파 값을 0으로 설정하여 숨김
    }

    public void onBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                Debug.Log("게임 시작");
                StartCoroutine(FadeInBlackScreen());
                break;
            case BTNType.Tutorial:
                Debug.Log("게임 방법");
                StartCoroutine(FadeInTutorialScreen());
                break;
        }
    }

    private IEnumerator FadeInTutorialScreen()
    {
        tutorialScreen.SetActive(true); // 튜토리얼 화면 활성화

        tutorialCanvasGroup.alpha = 0f; // Set initial alpha to 0

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            tutorialCanvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tutorialCanvasGroup.alpha = 1f; // Ensure alpha value is set to 1
    }

    private IEnumerator FadeInBlackScreen()
    {
        tutorialScreen.SetActive(false);
        startStory.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}