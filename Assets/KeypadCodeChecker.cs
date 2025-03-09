using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadCodeChecker : MonoBehaviour
{
    public Image errorScreen;
    public GameObject symbolSlot1;
    public GameObject symbolSlot2;
    public GameObject symbolSlot3;
    public GameObject symbolSlot4;

    // symbol
    Image symbolInput1;
    Image symbolInput2;
    Image symbolInput3;
    Image symbolInput4;

    public GameObject exit;

    Sprite[] password = new Sprite[4];

    void Awake() {
        Sprite symbol1 = Resources.Load<Sprite>("KeypadSymbol1");
        Sprite symbol2 = Resources.Load<Sprite>("KeypadSymbol2");
        Sprite symbol3 = Resources.Load<Sprite>("KeypadSymbol3");
        password[0] = symbol1;
        password[1] = symbol2;
        password[2] = symbol3;
        password[3] = symbol1;

        symbolInput1 = symbolSlot1.GetComponent<Image>();
        symbolInput2 = symbolSlot2.GetComponent<Image>();
        symbolInput3 = symbolSlot3.GetComponent<Image>();
        symbolInput4 = symbolSlot4.GetComponent<Image>();
    }

    public void OnSubmitButtonPress() {
        if (symbolInput1.sprite == password[0] &&
            symbolInput2.sprite == password[1] &&
            symbolInput3.sprite == password[2] &&
            symbolInput4.sprite == password[3]
        ) {
            errorScreen.color = new Color(0,255,0,50);
            exit.GetComponent<ExitScript>().ExitOpen();
        } else {
            FlashingRed();
        }
    }

    IEnumerator FlashingRed() {
        errorScreen.color = new Color(50, 50, 50, 50);
        yield return new WaitForSeconds(0.25f);
        errorScreen.color = new Color(255, 0, 0, 50);
        yield return new WaitForSeconds(0.25f);
        errorScreen.color = new Color(50, 50, 50, 50);
        yield return new WaitForSeconds(0.25f);
        errorScreen.color = new Color(255, 0, 0, 50);
        yield return new WaitForSeconds(0.25f);
    }
}
