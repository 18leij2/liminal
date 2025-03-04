using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float moveAmount = 5f;

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

    private void ExitOpen()
    {
        left.transform.Translate(0, 0, -moveAmount);
        right.transform.Translate(0, 0, moveAmount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
