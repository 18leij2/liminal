using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float moveAmount = 5f; // How far each door moves to open
    bool exitOpening = false;
    float leftInitialZ;
    float rightInitialZ;

    private bool isNearDoor = false;
    private bool doorOpened = false; // Prevents multiple triggers

    private void OnEnable()
    {
        PlayerController.OpenDoors += ExitOpen;
        RootMotionControlScript.OpenDoors += ExitOpen;
    }

    private void OnDisable()
    {
        PlayerController.OpenDoors -= ExitOpen;
        RootMotionControlScript.OpenDoors -= ExitOpen;
    }

    void Start()
    {
        leftInitialZ = left.transform.position.z;
        rightInitialZ = right.transform.position.z;
    }

    void Update()
    {
        // Handle door animation when opening
        if (exitOpening)
        {
            left.transform.Translate(0, 0, -0.05f); // Adjust speed as needed
            right.transform.Translate(0, 0, 0.05f); // Adjust speed as needed

            // Stop moving when reaching target position
            if (left.transform.position.z <= leftInitialZ - moveAmount)
            {
                exitOpening = false;
            }
        }

        // Check if player is near and has key
        if (isNearDoor && !doorOpened)
        {
            Debug.Log("Player is near the door, attempting to open...");
            TryOpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected near the exit door!");
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
            Debug.Log("Checking inventory for Key: " + hasKey);

            if (hasKey)
            {
                ExitOpen(); // Trigger door opening
                doorOpened = true; // Prevent repeat opening
            }
            else
            {
                Debug.Log("Key not found in inventory!");
            }
        }
        else
        {
            Debug.Log("InventoryManager.Singleton is null");
        }
    }

    public void ExitOpen()
    {
        Debug.Log("Exit Door Opening!");
        exitOpening = true; // Trigger smooth opening in Update()
    }
}
