using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public float gapDuration = 1.0f; // Gap duration in seconds

    private AudioSource audioSource;
    private float loopStartTime;

    public GameObject footStep;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = footStep.GetComponent<AudioSource>();
        audioSource.loop = false;
        loopStartTime = Time.time;
        audioSource.Play();

        footStep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (Time.time - loopStartTime >= gapDuration)
            {
                audioSource.Play();
                loopStartTime = Time.time;
            }
        }

        if(Input.GetKeyDown("w"))
        {
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("a"))
        {
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("s"))
        {
            footStep.SetActive(true);
        }
        if(Input.GetKeyDown("d"))
        {
            footStep.SetActive(true);
        }
        if(Input.GetKeyUp("w"))
        {
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("a"))
        {
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("s"))
        {
            footStep.SetActive(false);
        }
        if(Input.GetKeyUp("d"))
        {
            footStep.SetActive(false);
        }
        
    }
}
