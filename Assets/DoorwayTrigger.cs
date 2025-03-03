using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    public GameObject endSceneUI;  // Reference to the UI panel that shows the buttons.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure it only triggers when the player enters.
        {
            endSceneUI.SetActive(true);  // Activate the UI with the choices.
        }
    }
}
