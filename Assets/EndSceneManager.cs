using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class EndSceneManager : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public GameObject screenOverlay; 
    public AudioSource victoryMusic;
    public AudioSource buzzingSound;
    public GameObject player;
    private bool isPlayerMoving = false;

    void Start() {
        screenOverlay.SetActive(false);
    }
    public void Escape()
    {
        screenOverlay.SetActive(true);
        screenOverlay.GetComponent<Image>().color = Color.white;
        //victoryMusic.Play();
        endText.color = Color.black;
        endText.text = "You escaped the back rooms… but something escaped with you. Can you feel it watching? Can you hear it breathing?";

        Invoke("EndGame", 5f);
    }

    public void Submission()
    {
        screenOverlay.SetActive(true);
        screenOverlay.GetComponent<Image>().color = Color.black;
        //buzzingSound.Play();
        endText.color = Color.white;
        endText.text = "You decided to become one with the void…like the rest of them did.";

        Invoke("EndGame", 5f);
    }

    public void Truth()
    {
        // Change screen to black and show the hallway scene.
        screenOverlay.SetActive(true);
        screenOverlay.GetComponent<Image>().color = Color.black;
        
        // Simulate player walking into the hallway.
        //isPlayerMoving = true;
        endText.color = Color.white;
        endText.text = "Your curiosity consumed you. You refused to leave without the answers you craved, but will it be worth the price?";

        Invoke("EndGame", 5f);
    }

    void Update()
    {
        if (isPlayerMoving)
        {
            // can animate the player's movement here or switch the camera to a fixed position
        }
    }

    void EndGame()
    {
        // SceneManager.LoadScene("EndScene");
        SceneManager.LoadScene(0);
    }
}
