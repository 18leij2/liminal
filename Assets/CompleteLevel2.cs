using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel2 : MonoBehaviour
{
    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            SceneManager.LoadScene("Level 3");
        }
    }
}
