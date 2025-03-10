using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialCanvas;
    private bool isTutorialVisible = true;

    void Start()
    {
        tutorialCanvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isTutorialVisible = !isTutorialVisible; // Toggle state
            tutorialCanvas.SetActive(isTutorialVisible); // Update UI
        }
    }
}
