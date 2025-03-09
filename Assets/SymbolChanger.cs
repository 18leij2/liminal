using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolChanger : MonoBehaviour
{
    public Sprite initialSymbol;
    Sprite[] symbols = new Sprite[6];
    Image image;

    void Awake() {
        symbols[0] = Resources.Load<Sprite>("KeypadSymbol1");
        symbols[1] = Resources.Load<Sprite>("KeypadSymbol2");
        symbols[2] = Resources.Load<Sprite>("KeypadSymbol3");
        symbols[3] = Resources.Load<Sprite>("KeypadSymbol4");
        symbols[4] = Resources.Load<Sprite>("KeypadSymbol5");
        symbols[5] = Resources.Load<Sprite>("KeypadSymbol6");

        image = gameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        image.sprite = initialSymbol;
    }

    public void OnKeypadButtonPress() {
        Sprite currentSymbol = image.sprite;
        Sprite newSymbol = PickRandomSymbol();
        while (newSymbol == currentSymbol) {
            newSymbol = PickRandomSymbol();
        }
        image.sprite = newSymbol;
    }

    Sprite PickRandomSymbol() {
        int index = Random.Range(0, symbols.Length);
        return symbols[index];
    }
}
