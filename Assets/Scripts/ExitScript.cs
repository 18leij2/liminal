using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float moveAmount = 5f;
    bool exitOpening = false;
    float leftInitialZ;
    float rightInitialZ;


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
            left.transform.Translate(0,0, -0.005f);
            right.transform.Translate(0,0, 0.005f);
        }
        if (left.transform.position.z <= leftInitialZ - moveAmount) {
            exitOpening = false;
        }
    }
}
