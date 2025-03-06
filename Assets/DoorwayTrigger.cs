using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    public GameObject endSceneUI; 
    void Start()
    {
        endSceneUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endSceneUI.SetActive(true);
        }
    }
}
