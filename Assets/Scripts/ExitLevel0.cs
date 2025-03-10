using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel0 : MonoBehaviour
{
    public GameObject leftDoor;  // Left part of the door
    public GameObject rightDoor; // Right part of the door
    public float moveAmount = 5f; // How far the doors should open
    public string nextSceneName = "Level 1"; // Name of the next scene

    private bool isNearDoor = false;
    private bool doorOpened = false; // To prevent multiple openings
    private bool exitOpening = false;

    private float leftInitialZ;
    private float rightInitialZ;

    void Start()
    {
        // Store the initial Z positions to control how far doors open
        leftInitialZ = leftDoor.transform.position.z;
        rightInitialZ = rightDoor.transform.position.z;
    }

    void Update()
    {
        // Animate door opening if triggered
        if (exitOpening)
        {
            leftDoor.transform.Translate(0, 0, -0.05f); // Adjust speed as needed
            rightDoor.transform.Translate(0, 0, 0.05f); // Adjust speed as needed

            // Stop moving when reaching target open distance
            if (leftDoor.transform.position.z <= leftInitialZ - moveAmount)
            {
                exitOpening = false;
                Debug.Log("Doors fully opened. Loading next level...");
                SceneManager.LoadScene(nextSceneName); // Load Level 1
            }
        }

        // Auto-check when near door and not already opened
        if (isNearDoor && !doorOpened)
        {
            TryOpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player near exit door.");
            isNearDoor = true;
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
        if (InventoryManager.Singleton != null)
        {
            bool hasKey = InventoryManager.Singleton.HasItem("Key");
            Debug.Log("Checking for Key: " + hasKey);

            if (hasKey)
            {
                ExitOpen(); // Start opening door
                doorOpened = true;
            }
            else
            {
                Debug.Log("Key not found. Door remains locked.");
            }
        }
        else
        {
            Debug.LogError("InventoryManager not found in scene.");
        }
    }

    private void ExitOpen()
    {
        Debug.Log("Opening exit door...");
        exitOpening = true; // Begin animation
    }
}
