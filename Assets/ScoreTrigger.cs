using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(">>> TRIGGER ENTER ÇALIŞTI <<<");
        Debug.Log("Çarpan obje: " + other.name);
        Debug.Log("Tag: " + other.tag);

        if (other.CompareTag("Ball"))
        {
            Debug.Log("🏀 BASKETTTTTTTTTTTTTT !!!!! 🏀");
        }
        else
        {
            Debug.Log("Ball TAG değil, bu yüzden basket sayılmadı!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger içinde duruyor: " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger EXIT: " + other.name);
    }
}
