using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel0 : MonoBehaviour
{

    private bool isNearDoor = false;
    private bool doorOpened = false; // Prevents repeated triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player near exit door.");
            isNearDoor = true;
            TryOpenDoor(); // Attempt to open the door when player enters trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
        }
    }

    private void TryOpenDoor()
    {
        if (doorOpened) return; // Prevent multiple triggers

        if (InventoryManager.Singleton != null)
        {
            bool hasKey = InventoryManager.Singleton.HasItem("Key");
            Debug.Log("Checking for Key: " + hasKey);

            if (hasKey)
            {
                doorOpened = true;
                Debug.Log("Key found! Loading next level...");
                SceneManager.LoadScene("Level 1");
            }
            else
            {
                Debug.Log("Key not found. Door remains locked.");
            }
        }
        else
        {
            Debug.LogError("InventoryManager.Singleton is null.");
        }
    }
}
