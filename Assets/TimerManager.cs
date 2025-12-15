using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public int totalTime = 180;
    private int currentTime;
    private bool gameEnded = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI winText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentTime = totalTime;

        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);

        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
        UpdateTimerText();
    }

    void UpdateTimer()
    {
        if (gameEnded) return;

        currentTime--;
        UpdateTimerText();

        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame(false);
        }
    }

    void UpdateTimerText()
    {
        int min = currentTime / 60;
        int sec = currentTime % 60;
        timerText.text = $"{min:00}:{sec:00}";
    }

    // 🔥 TÜM OBJELER YERLEŞİNCE
    public void ForceEndGame()
    {
        EndGame(true);
    }

    void EndGame(bool success)
    {
        gameEnded = true;
        CancelInvoke();

        if (success)
        {
            winText.gameObject.SetActive(true);

            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddRemainingTime(currentTime);
        }
        else
        {
            loseText.gameObject.SetActive(true);
        }
    }
}
