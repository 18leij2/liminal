using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] public InventorySlot[] inventorySlots;
    [SerializeField] public InventorySlot[] hotbarSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    void Awake()
    {
        Singleton = this;
    }

    void Update()
    {
        if(carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if(carriedItem != null)
        {
            item.activeSlot.SetItem(carriedItem);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if(_item == null)
        { _item = PickRandomItem(); }

        for (int i = 0; i < hotbarSlots.Length; i++) {
            if (hotbarSlots[i].myItem == null) {
                Instantiate(itemPrefab, hotbarSlots[i].transform).Initialize(_item, hotbarSlots[i]);
                return;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if the slot is empty
            if(inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                return;
            }
        }
    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }

    public bool HasItem(string itemName)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem != null &&
                slot.myItem.myItem.itemName == itemName)
            {
                return true;
            }
        }
        foreach (InventorySlot slot in hotbarSlots) {
            if (slot.myItem != null && slot.myItem.myItem != null &&
                slot.myItem.myItem.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }


}