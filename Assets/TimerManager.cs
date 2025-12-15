using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float totalTime = 180f;
    private float currentTime;
    private bool gameEnded = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject loseText;
    public GameObject winText;

    private void Start()
    {
        currentTime = totalTime;

        if (loseText != null) loseText.SetActive(false);
        if (winText != null) winText.SetActive(false);
    }

    private void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            LoseGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{min:00}:{sec:00}";
    }

    void LoseGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (loseText != null)
            loseText.SetActive(true);
    }

    // 🔥 İŞTE ARADIĞIN FONKSİYON
    public void WinGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        // Timer durur
        enabled = false;

        if (loseText != null)
            loseText.SetActive(false);

        if (winText != null)
            winText.SetActive(true);

        // Kalan süre skora eklenir
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddRemainingTime(Mathf.CeilToInt(currentTime));
    }
}
