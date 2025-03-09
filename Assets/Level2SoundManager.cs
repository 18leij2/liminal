using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2SoundManager : MonoBehaviour
{

    public AudioSource largeBuzzing1;
    public AudioSource largeBuzzing2;
    public AudioSource mediumBuzz;
    public AudioSource weirdBuzz;
    public AudioSource playerCapture;
    public AudioSource ambient;
    AudioSource[] ambienceAudio = new AudioSource[4];
    public float timer;
    float interval;
    float minSoundInterval = 15f;
    float maxSoundInterval = 45f;

    void Start() {
        ambient.Play();
        timer = 0f;
        ambienceAudio[0] = largeBuzzing1;
        ambienceAudio[1] = largeBuzzing2;
        ambienceAudio[2] = mediumBuzz;
        ambienceAudio[3] = weirdBuzz;
        interval = Random.Range(minSoundInterval, maxSoundInterval);
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > interval) {
            PlayRandomSound();
            timer = 0f;
            interval = Random.Range(minSoundInterval, maxSoundInterval);
        }
    }

    void PlayRandomSound() {
        int index = Random.Range(0, ambienceAudio.Length);
        ambienceAudio[index].Play();
    }

    void Level2Ambience() {

    }

    public void PlayLargeBuzzing1() {
        largeBuzzing1.Play();
    }

    public void PlayLargeBuzzing2() {
        largeBuzzing2.Play();
    }

    public void PlayMediumBuzz() {
        mediumBuzz.Play();
    }

    public void PlayWeirdBuzz() {
        mediumBuzz.Play();
    }

    public void PlayPlayerCapture() {
        mediumBuzz.Play();
    }
}
