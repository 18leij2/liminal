using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioOpener : MonoBehaviour
{
    public GameObject hotbarObject;
    public GameObject hotbarSlotsObject;
    public CanvasGroup hotbarSlotsGroup;
    public CanvasGroup inventoryGroup;
    public CanvasGroup radioGroup;
    public CanvasGroup hintGroup;
    public CanvasGroup paperHintGroup;
    public TMP_Text radioText;
    public TMP_Text hintText;
    public int radioNumber;

    bool canOpenRadio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpenRadio) {
                if (radioGroup.alpha == 0f) {
                    OpenRadio();
                } else {
                    CloseRadio();
                }
            }
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            canOpenRadio = true;
            hintGroup.alpha = 1f;
            hintGroup.interactable = true;
            hintGroup.blocksRaycasts = true;
            if (radioNumber == 0) {
                radioText.text = "\"... The Backrooms is a fictional, liminal space... One can unintentionally enter this space by no clipping out of reality... But what does that even mean?\"";
            } else if (radioNumber == 1) {
                radioText.text = "\"... God save you if your hear something wandering around nearby... Because it sure as hell has heard you.\"";
            } else if (radioNumber == 2) {
                radioText.text = "... I don't know how I got here, but there seems to be no end to this place... Is there any way out? Is anyone out there? ... Hello?\"";
            } else if (radioNumber == 3) {
                radioText.text = "... Who made this place? How did it get like this? ... Who was here before me? ... Is anyone coming to look for me?\"";
            }
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.gameObject.tag == "Player") {
            canOpenRadio = false;
            if (radioGroup.alpha == 1f) {
                CloseRadio();
            }
            hintGroup.alpha = 0f;
            hintGroup.interactable = false;
            hintGroup.blocksRaycasts = false;
        }
    }

    void OpenRadio() {
        paperHintGroup.alpha = 0f;
        paperHintGroup.interactable = false;
        paperHintGroup.blocksRaycasts = false;

        radioGroup.alpha = 1f;
        radioGroup.interactable = true;
        radioGroup.blocksRaycasts = true;

        if (inventoryGroup.alpha == 1f) {
            inventoryGroup.alpha = 0f;
            inventoryGroup.interactable = false;
            inventoryGroup.blocksRaycasts = false;
        } else {
            hotbarObject.SetActive(false);
        }
        hotbarSlotsGroup.alpha = 0f;

        hintText.text = "Press E to close";
    }
    
    void CloseRadio() {
        radioGroup.alpha = 0f;
        radioGroup.interactable = false;
        radioGroup.blocksRaycasts = false;

        hotbarObject.SetActive(true);
        hotbarSlotsGroup.alpha = 1f;

        hintText.text = "Press E to interact";
        paperHintGroup.alpha = 1f;
        paperHintGroup.interactable = true;
        paperHintGroup.blocksRaycasts = true;
    }
}
