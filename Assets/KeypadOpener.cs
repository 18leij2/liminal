using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadOpener : MonoBehaviour
{
    public GameObject hotbarObject;
    public GameObject hotbarSlotsObject;
    public CanvasGroup hotbarSlotsGroup;
    public CanvasGroup inventoryGroup;
    public CanvasGroup keypadGroup;
    public CanvasGroup hintGroup;

    bool canUseKeypad = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseKeypad) {
                Debug.Log("player using keypad");
                if (keypadGroup.alpha == 0f) {
                    OpenKeypad();
                } else {
                    CloseKeypad();
                }
            }
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            Debug.Log("player can use keypad");
            canUseKeypad = true;
            hintGroup.alpha = 1f;
            hintGroup.interactable = true;
            hintGroup.blocksRaycasts = true;
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.gameObject.tag == "Player") {
            Debug.Log("player cant use keypad");
            canUseKeypad = false;
            if (keypadGroup.alpha == 1f) {
                CloseKeypad();
            }
            hintGroup.alpha = 0f;
            hintGroup.interactable = false;
            hintGroup.blocksRaycasts = false;
        }
    }

    void OpenKeypad() {
        hintGroup.alpha = 0f;
        hintGroup.interactable = false;
        hintGroup.blocksRaycasts = false;

        keypadGroup.alpha = 1f;
        keypadGroup.interactable = true;
        keypadGroup.blocksRaycasts = true;

        if (inventoryGroup.alpha == 1f) {
            inventoryGroup.alpha = 0f;
            inventoryGroup.interactable = false;
            inventoryGroup.blocksRaycasts = false;
        } else {
            hotbarObject.SetActive(false);
        }
        hotbarSlotsGroup.alpha = 0f;
    }

    void CloseKeypad() {
        keypadGroup.alpha = 0f;
        keypadGroup.interactable = false;
        keypadGroup.blocksRaycasts = false;

        hotbarObject.SetActive(true);
        hotbarSlotsGroup.alpha = 1f;
        hotbarSlotsObject.transform.localPosition = new Vector3(0f, -120f, 0f);
    }
}
