using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleUI : MonoBehaviour
{   
    public static SubtitleUI Instance;
    [SerializeField] private TMP_Text subtitleText; // Inspector에서 넣기

    [Header("Tutorial Settings")]
    private float lineDuration = 3f;
    private float gapBetweenLines = 0.2f;

    private Coroutine running;

    private void Awake()
    {
        Instance = this;
        
        // 혹시 Inspector에서 안 넣었을 경우 자동으로 찾기
        if (subtitleText == null)
            subtitleText = GetComponentInChildren<TMP_Text>();

        if (subtitleText == null)
            Debug.LogError("SubtitleUI: subtitleText가 할당되지 않았습니다!");
    }

    void Start()
    {
        // 튜토리얼 자막
        string[] tutorialLines =
        {
            "이동: L컨트롤러 스틱",
            "물건: R컨트롤러 Grab 버튼",
            "상호작용: R컨트롤러 A버튼"
        };

        ShowLines(tutorialLines, lineDuration, gapBetweenLines);
    }

    // 여러 줄 순차 출력
    public void ShowLines(string[] lines, float durationPerLine, float gap = 0f)
    {
        if (subtitleText == null)
        {
            Debug.LogWarning("SubtitleUI: subtitleText가 null이라 ShowLines 실행 불가.");
            return;
        }

        if (running != null)
            StopCoroutine(running);

        running = StartCoroutine(CoShowLines(lines, durationPerLine, gap));
    }

    private IEnumerator CoShowLines(string[] lines, float durationPerLine, float gap)
    {
        subtitleText.gameObject.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            subtitleText.text = lines[i];
            yield return new WaitForSeconds(durationPerLine);

            if (gap > 0f)
                yield return new WaitForSeconds(gap);
        }

        subtitleText.text = "";
        

        running = null;
    }
}
