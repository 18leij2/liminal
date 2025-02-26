using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    // public InventoryManager im;
    public Item keyItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            InventoryManager.Singleton.SpawnInventoryItem(keyItem);
            this.gameObject.SetActive(false);
        }
    }
}
