using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int scorePerObject = 100;
    public int totalObjects = 5;

    private int placedCount = 0;
    private int record;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // OYUN BAŞINDA REKORU OKU
        record = PlayerPrefs.GetInt("RECORD", 0);
        UpdateUI();
    }

    public void AddScore()
    {
        score += scorePerObject;
        placedCount++;

        CheckRecord();   // 🔥 ANINDA REKOR KONTROL
        UpdateUI();

        if (placedCount >= totalObjects)
        {
            TimerManager tm = FindObjectOfType<TimerManager>();
            if (tm != null)
                tm.WinGame();
        }
    }

    public void AddRemainingTime(int seconds)
    {
        score += seconds;

        CheckRecord();   // 🔥 KALAN SÜRE DE REKORA DAHİL
        UpdateUI();
    }

    private void CheckRecord()
    {
        if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("RECORD", record);
            PlayerPrefs.Save();
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SKOR: " + score;

        if (recordText != null)
            recordText.text = "REKOR: " + record;
    }
}
