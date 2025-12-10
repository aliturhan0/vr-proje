using UnityEngine;

public class GoalZoneTrigger : MonoBehaviour
{
    bool showMessage = false;
    float messageTimer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kutu"))
        {
            showMessage = true;
            messageTimer = 2f; // 2 saniye göster
        }
    }

    private void Update()
    {
        if (showMessage)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
                showMessage = false;
        }
    }

    private void OnGUI()
    {
        if (showMessage)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.green;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200),
                "GÖREV TAMAMLANDI!",
                style
            );
        }
    }
}
