using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeFootstep : MonoBehaviour
{
    public AudioClip[] steps;
    public Animator animator;
    private float lastFootstep;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var footstep = animator.GetFloat("Footstep");
        if (lastFootstep > 0 && footstep < 0 || lastFootstep < 0 && footstep > 0)
        {
            var randomStep = steps[Random.Range(0, steps.Length - 1)];
            AudioSource.PlayClipAtPoint(randomStep, transform.position);
        }
        lastFootstep = footstep;
    }
}
