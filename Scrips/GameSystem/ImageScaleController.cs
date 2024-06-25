using UnityEngine;
using System.Collections;

public class ImagePulse : MonoBehaviour
{
    public RectTransform imageTransform; // 애니메이션을 적용할 이미지의 RectTransform
    public float duration = 1.0f;        // 한 사이클의 지속 시간

    private Vector2 originalSize;
    private Vector2 targetSize;

    private void Start()
    {
        if (imageTransform == null)
        {
            imageTransform = GetComponent<RectTransform>();
        }

        // 원래 크기를 저장
        originalSize = imageTransform.sizeDelta;
        // 타겟 크기는 원래 크기의 80%
        targetSize = originalSize * 0.8f;

        // 크기 변화 시작
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        while (true)
        {
            // 작아지기
            yield return StartCoroutine(ScaleTo(targetSize, duration / 2));
            // 커지기
            yield return StartCoroutine(ScaleTo(originalSize, duration / 2));
        }
    }

    private IEnumerator ScaleTo(Vector2 targetSize, float time)
    {
        Vector2 startSize = imageTransform.sizeDelta;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            imageTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageTransform.sizeDelta = targetSize;
    }
}
