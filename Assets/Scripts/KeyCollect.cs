using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class KeyCollect : MonoBehaviour
{
    public static int keyCount = 0;
    public TMP_Text keyCounterText; // Change from Text to TMP_Text

    private void Start()
    {
        UpdateKeyUI(); // Ensure the UI starts correctly
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Key Collected!");
            keyCount++;
            UpdateKeyUI();
            Destroy(gameObject);
        }
    }

    private void UpdateKeyUI()
    {
        if (keyCounterText != null)
        {
            keyCounterText.text = "Keys Collected: " + keyCount + "/3";

            if (keyCount >= 3)
            {
                keyCounterText.text = "Ready to Exit!";
            }
        }
        else
        {
            Debug.LogError("KeyCounterText UI is not assigned in the Inspector!");
        }
    }
}
