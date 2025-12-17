using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarryObjectID : MonoBehaviour
{
    [Header("ID")]
    public int objectID;

    [HideInInspector]
    public bool placedCorrectly = false;

    // 🔒 Başlangıç durumu (Replay için)
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Rigidbody rb;
    private XRGrabInteractable grab;

    private void Awake()
    {
        // Başlangıç konumunu kaydet
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    // =========================
    // DropZone doğru yerleştirince çağrılıyor
    // =========================
    public void LockObject()
    {
        placedCorrectly = true;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (grab != null)
            grab.enabled = false;
    }

    // =========================
    // 🔥 Replay / Yeni Oyun için RESET
    // =========================
    public void ResetObject()
    {
        placedCorrectly = false;

        // Konum & rotasyon sıfırla
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Fizik geri aç
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Grab geri aç
        if (grab != null)
            grab.enabled = true;
    }
}
