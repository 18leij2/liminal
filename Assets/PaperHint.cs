using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperHint : MonoBehaviour
{

    public TMP_Text paperHintText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHint();
    }

    public void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            UpdateHint();
        }
    }

    public void UpdateHint() {
        int count = 0;
        foreach (InventorySlot slot in InventoryManager.Singleton.inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem != null &&
                (slot.myItem.myItem.itemName == "PaperClue1" || 
                slot.myItem.myItem.itemName == "PaperClue2" || 
                slot.myItem.myItem.itemName == "PaperClue3" || 
                slot.myItem.myItem.itemName == "PaperClue4"))
            {
                count++;
            }
        }
        foreach (InventorySlot slot in InventoryManager.Singleton.hotbarSlots) {
            if (slot.myItem != null && slot.myItem.myItem != null &&
                (slot.myItem.myItem.itemName == "PaperClue1" || 
                slot.myItem.myItem.itemName == "PaperClue2" || 
                slot.myItem.myItem.itemName == "PaperClue3" || 
                slot.myItem.myItem.itemName == "PaperClue4"))
            {
                count++;
            }
        }
        paperHintText.text = "Papers Collected: " + count + "/4";
    }
}
