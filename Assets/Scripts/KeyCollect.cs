using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class KeyCollect : MonoBehaviour
{
    public static int keyCount = 0;
    public TMP_Text keyCounterText; // Change from Text to TMP_Text
    public CanvasGroup textCanvasGroup;

    private void Start()
    {
        keyCount = 0;
        UpdateKeyUI(); // Ensure the UI starts correctly
        textCanvasGroup.alpha = 1f;
        textCanvasGroup.blocksRaycasts = true;
        textCanvasGroup.interactable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // textCanvasGroup.alpha = 1f;
            // textCanvasGroup.blocksRaycasts = true;
            // textCanvasGroup.interactable = true;
            // Invoke("HideText", 3f);
            // Debug.Log("Key Collected!");
            keyCount++;
            UpdateKeyUI();
            Destroy(gameObject);
        }
    }

    // private void HideText() {
    //     Debug.Log("hiding text");
    //     textCanvasGroup.alpha = 0f;
    //     textCanvasGroup.blocksRaycasts = false;
    //     textCanvasGroup.interactable = false;
    // }

    private void UpdateKeyUI()
    {
        Debug.Log("updating key ui");
        int count = 0;
        foreach (InventorySlot slot in InventoryManager.Singleton.inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem != null)
            {
                Debug.Log(slot.myItem.myItem.itemName);
            }
            if (slot.myItem != null && slot.myItem.myItem != null &&
                (slot.myItem.myItem.itemName == "Key3"))
            {
                count++;
            }
        }
        foreach (InventorySlot slot in InventoryManager.Singleton.hotbarSlots) {
            if (slot.myItem != null && slot.myItem.myItem != null)
            {
                Debug.Log(slot.myItem.myItem.itemName);
            }
            if (slot.myItem != null && slot.myItem.myItem != null &&
                (slot.myItem.myItem.itemName == "Key3"))
            {
                count++;
            }
        }
        if (keyCounterText != null)
        {
            keyCounterText.text = "Keys Collected: " + count + "/3";

            if (count >= 3)
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
