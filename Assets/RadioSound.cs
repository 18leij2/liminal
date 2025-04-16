using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSound : MonoBehaviour
{
    AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "Player") {
            src.Play();
        }
    }
    
    void OnTriggerExit(Collider c) {
        if (c.gameObject.tag == "Player") {
            src.Stop();
        }
    }
}
