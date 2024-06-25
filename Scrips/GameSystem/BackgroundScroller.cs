using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 20f; // 배경이 스크롤되는 속도
    public RectTransform background1;
    public RectTransform background2;

    private float backgroundWidth;

    void Start()
    {
        backgroundWidth = background1.rect.width;
    }

    void Update()
    {
        // 배경을 왼쪽으로 이동
        background1.anchoredPosition -= new Vector2(scrollSpeed * Time.deltaTime, 0);
        background2.anchoredPosition -= new Vector2(scrollSpeed * Time.deltaTime, 0);

        // 배경이 화면 밖으로 나가면 위치를 재설정
        if (background1.anchoredPosition.x < -backgroundWidth)
        {
            background1.anchoredPosition = new Vector2(background2.anchoredPosition.x + backgroundWidth, background1.anchoredPosition.y);
        }
        if (background2.anchoredPosition.x < -backgroundWidth)
        {
            background2.anchoredPosition = new Vector2(background1.anchoredPosition.x + backgroundWidth, background2.anchoredPosition.y);
        }
    }
}
