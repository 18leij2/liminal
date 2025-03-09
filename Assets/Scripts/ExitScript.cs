using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float moveAmount = 5f;
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

<<<<<<< HEAD
    private void Update()
    {
        if (isNearDoor && !doorOpened) // Check if player is near and door is not already open
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
                ExitOpen();
                doorOpened = true;
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


    private void ExitOpen()
    {
        left.transform.Translate(0, 0, -moveAmount);
        right.transform.Translate(0, 0, moveAmount);
        Debug.Log("Exit Door Opened!");
=======
    public void ExitOpen()
    {
        // left.transform.Translate(0, 0, -moveAmount);
        // right.transform.Translate(0, 0, moveAmount);
        exitOpening = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftInitialZ = left.transform.position.z;
        rightInitialZ = left.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (exitOpening) {
            left.transform.Translate(0,0, -0.001f);
            right.transform.Translate(0,0, 0.001f);
        }
        if (left.transform.position.z <= leftInitialZ - moveAmount) {
            exitOpening = false;
        }
>>>>>>> main
    }
}
