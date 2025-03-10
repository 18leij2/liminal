using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            InventoryManager.Singleton.SpawnInventoryItem(item);
            this.gameObject.SetActive(false);
        }
    }
}
