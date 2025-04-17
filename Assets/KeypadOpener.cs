using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadOpener : MonoBehaviour
{
    public GameObject hotbarObject;
    public GameObject hotbarSlotsObject;
    public CanvasGroup hotbarSlotsGroup;
    public CanvasGroup inventoryGroup;
    public CanvasGroup keypadGroup;
    public CanvasGroup keypadHintGroup;
    public CanvasGroup paperHintGroup;
    public TMP_Text keypadHintText;

    private bool reading;

    bool canUseKeypad = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseKeypad) {
                if (keypadGroup.alpha == 0f) {
                    OpenKeypad();
                } else {
                    CloseKeypad();
                }
            }
        
        if (reading)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // constantly disabling cursor interferes with pause
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            canUseKeypad = true;
            keypadHintGroup.alpha = 1f;
            keypadHintGroup.interactable = true;
            keypadHintGroup.blocksRaycasts = true;
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.gameObject.tag == "Player") {
            canUseKeypad = false;
            if (keypadGroup.alpha == 1f) {
                CloseKeypad();
            }
            keypadHintGroup.alpha = 0f;
            keypadHintGroup.interactable = false;
            keypadHintGroup.blocksRaycasts = false;
        }
    }

    void OpenKeypad() {
        reading = true;
        paperHintGroup.alpha = 0f;
        paperHintGroup.interactable = false;
        paperHintGroup.blocksRaycasts = false;

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

        keypadHintText.text = "Press E to close";
    }

    void CloseKeypad() {
        reading = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        keypadGroup.alpha = 0f;
        keypadGroup.interactable = false;
        keypadGroup.blocksRaycasts = false;

        hotbarObject.SetActive(true);
        hotbarSlotsGroup.alpha = 1f;

        keypadHintText.text = "Press E to interact";
        paperHintGroup.alpha = 1f;
        paperHintGroup.interactable = true;
        paperHintGroup.blocksRaycasts = true;
        // hotbarSlotsObject.transform.localPosition = new Vector3(0f, -120f, 0f);
    }
}