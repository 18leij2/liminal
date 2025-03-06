using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minFlickerSpeed;
    public float maxFlickerSpeed;
    public float timer;
    public float maxIntensity = 1f;
    float interval;

    void Start() {
        timer = 0f;
        interval = Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval){
            if (this.gameObject.GetComponent<Light>().intensity == 0f) {
                this.gameObject.GetComponent<Light>().intensity = Random.Range(0f, maxIntensity);
            } else {
                this.gameObject.GetComponent<Light>().intensity = 0f;
            }
            timer = 0f;
            interval = Random.Range(minFlickerSpeed, maxFlickerSpeed);
        }

        // this.gameObject.SetActive(true);
        // yield WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        // this.gameObject.SetActive(false);
        // yield WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
    }
}
