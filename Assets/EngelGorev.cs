using UnityEngine;

public class EngelGorev : MonoBehaviour
{
    public Transform takipNoktasi;   // Main Camera
    public float engelSonZ = 2.5f;   // Engelin arkasýndaki Z
    public float engelOnZ = 1.5f;    // Engelin ön tarafý (reset bölgesi)

    bool gecti = false;

    bool showMessage = false;
    float timer = 0f;

    void Update()
    {
        // Arkaya geçtiyse ve daha önce geçmediyse
        if (!gecti && takipNoktasi.position.z > engelSonZ)
        {
            gecti = true;
            showMessage = true;
            timer = 2f;  // 2 saniye göster
        }

        // Oyuncu tekrar engelin önüne gelince resetle
        if (gecti && takipNoktasi.position.z < engelOnZ)
        {
            gecti = false;
        }

        // Mesaj zamanlayýcý
        if (showMessage)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                showMessage = false;
        }
    }

    void OnGUI()
    {
        if (showMessage)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.cyan;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300),
                "ENGELÝ GEÇTÝN!",
                style
            );
        }
    }
}
