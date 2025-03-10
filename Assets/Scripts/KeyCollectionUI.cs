using TMPro;
using UnityEngine;

public class KeyCollectionUI : MonoBehaviour
{
    public TMP_Text keyCounterText; // Use TMP_Text instead of Text
    private int keysCollected = 0;
    private int totalKeys = 3;

    void Start()
    {
        UpdateKeyText();
    }

    public void CollectKey()
    {
        keysCollected++;
        UpdateKeyText();
    }

    private void UpdateKeyText()
    {
        if (keysCollected < totalKeys)
        {
            keyCounterText.text = "Keys Collected: " + keysCollected + "/" + totalKeys;
        }
        else
        {
            keyCounterText.text = "Ready to Exit!";
        }
    }
}

