using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropZoneID : MonoBehaviour
{
    public int targetID;
    private bool zoneCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        CarryObjectID obj = other.GetComponent<CarryObjectID>();
        if (obj == null) return;

        // 🔒 Bu dropzone zaten tamamlandıysa
        if (zoneCompleted) return;

        // ❌ Obje daha önce başka yere yerleştirildiyse
        if (obj.placedCorrectly) return;

        // ❌ Yanlış obje
        if (obj.objectID != targetID) return;

        // ✅ DOĞRU OBJE + İLK KEZ
        Debug.Log("PUAN VERILIYOR");

        obj.placedCorrectly = true;
        zoneCompleted = true;

        // Snap
        Vector3 snapPos = transform.position;
        snapPos.y += transform.localScale.y / 2f;
        snapPos.y += other.transform.localScale.y / 2f;
        other.transform.position = snapPos;

        // Fizik kapat
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // Grab kapat
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;

        // Skor
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScoreWithEffect();
    }
}
