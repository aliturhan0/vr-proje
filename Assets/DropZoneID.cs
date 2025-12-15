using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropZoneID : MonoBehaviour
{
    public int targetID;

    private void OnTriggerEnter(Collider other)
    {
        CarryObjectID obj = other.GetComponent<CarryObjectID>();
        if (obj == null) return;

        // SADECE DOĞRU OBJE VE İLK KEZ
        if (obj.objectID == targetID && !obj.placedCorrectly)
        {
            obj.placedCorrectly = true;

            // OBJENİN YERİNE OTURMASI
            Vector3 snapPos = transform.position;
            snapPos.y += transform.localScale.y / 2f;
            snapPos.y += other.transform.localScale.y / 2f;
            other.transform.position = snapPos;

            // FİZİĞİ KAPAT
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // TEKRAR TUTULAMASIN
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                grab.enabled = false;
            }

            // 🔥 PUAN VER
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore();
            }
        }
    }
}
