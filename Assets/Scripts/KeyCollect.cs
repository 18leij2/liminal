using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public static int keyCount = 0; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Key Collected!");
            keyCount++;
            Debug.Log("Total Keys: " + keyCount);
            Destroy(gameObject);
        }
    }
}
