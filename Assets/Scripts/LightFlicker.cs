using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code borrowed from https://gamedevbeginner.com/how-to-make-a-light-flicker-in-unity/
// Switched so light was off and flickered on in stead of the opposite.

public class LightFlicker : MonoBehaviour
{
    public Light myLight;
    public float maxInterval = 1;
    public float maxFlicker = 0.2f;

    float defaultIntensity;
    bool isOn;
    float timer;
    float delay;

    private void Start()
    {
        defaultIntensity = myLight.intensity;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        isOn = !isOn;

        if (isOn)
        {
            myLight.intensity = defaultIntensity;
            delay = Random.Range(0, maxInterval);
        }
        else
        {
            //Line changed so that light was off and flickers on to maximum 1.5f intensity
            myLight.intensity = Random.Range(defaultIntensity, 1.5f);
            delay = Random.Range(0, maxFlicker);
        }

        timer = 0;
    }
}
