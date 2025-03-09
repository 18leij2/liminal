using UnityEngine;
using UnityEngine.SceneManagement; 

public class ExitTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && KeyCollect.keyCount >= 3)
        {
            Debug.Log("You have 3 keys! Level Complete!");
            SceneManager.LoadScene("Level 2");  
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Find more keys to exit!");
        }
    }
}
