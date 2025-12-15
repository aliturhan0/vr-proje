using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Settings")]
    public int score = 0;
    public int scorePerObject = 100;
    public int totalObjects = 5;
    private int placedCount = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI floatingScoreText;
    public float floatDuration = 0.6f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();

        if (floatingScoreText != null)
            floatingScoreText.gameObject.SetActive(false);
    }

    // 🔥 DropZone çağırır
    public void AddScoreWithEffect()
    {
        placedCount++;
        StartCoroutine(FloatingScoreRoutine());

        // ✅ TÜM OBJELER YERLEŞTİ → OYUN BİTSİN
        if (placedCount >= totalObjects)
        {
            if (TimerManager.Instance != null)
                TimerManager.Instance.ForceEndGame();
        }
    }

    // ⏱ Timer kalan süreyi buradan ekler
    public void AddRemainingTime(int seconds)
    {
        score += seconds;
        UpdateScoreText();
    }

    private IEnumerator FloatingScoreRoutine()
    {
        floatingScoreText.gameObject.SetActive(true);
        floatingScoreText.text = "+" + scorePerObject;

        RectTransform floatRT = floatingScoreText.rectTransform;
        RectTransform scoreRT = scoreText.rectTransform;

        Vector3 startPos = floatRT.position;
        Vector3 endPos = scoreRT.position;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / floatDuration;
            floatRT.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        score += scorePerObject;
        UpdateScoreText();

        floatRT.position = startPos;
        floatingScoreText.gameObject.SetActive(false);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "SKOR: " + score;
    }
}
