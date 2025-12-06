using UnityEngine;
using TMPro;
using System.Collections; // IEnumerator 필요

public class SubtitleManager : MonoBehaviour
{
    // 싱글톤
    public static SubtitleManager Instance;

    [Header("Subtitle Text Reference")]
    public TMP_Text subtitleText;

    [Header("Subtitle Settings")]
    public float defaultDuration = 2f;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 처음에는 텍스트 비우기
        if (subtitleText != null)
            subtitleText.text = "";
        else
            Debug.LogError("Subtitle Text가 연결되지 않았습니다!");
    }

    private void Start()
    {
        // 테스트용: Start에서 바로 3초간 hello 표시
        StartCoroutine(ShowTest());
        ShowSubtitle("이거 3초간 보여요", 3f);

    }

    private IEnumerator ShowTest()
    {
        yield return new WaitForSeconds(0.1f); // XR 씬 로딩 안정용
        ShowSubtitle("hello", 3f);
    }

    /// <summary>
    /// 자막 표시 (기본 duration)
    /// </summary>
    public void ShowSubtitle(string message)
    {
        ShowSubtitle(message, defaultDuration);
    }

    /// <summary>
    /// 자막 표시 (지정한 duration)
    /// </summary>
    public void ShowSubtitle(string message, float duration)
    {
        if (subtitleText == null)
        {
            Debug.LogWarning("Subtitle Text가 할당되지 않았습니다!");
            return;
        }

        StopAllCoroutines(); // 이전 자막 제거
        subtitleText.text = message;
        StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        subtitleText.text = "";
    }
}
