using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBehavior : MonoBehaviour
{
    private bool hasBeenSnuffed = false; // Flag to prevent double counting
    private GameObject player;

    public GameObject litCandle;
    public GameObject flame;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !hasBeenSnuffed)
        {
            float distance = Vector3.Distance(litCandle.transform.position, player.transform.position);
            if (distance < 2.0f) //can adjust range later
            {
                SnuffCandle();
            }
        }
    }

    // change animation state here maybe?
    private void SnuffCandle()
    {
        // replace with audio sound for candle being put out
        //AudioSource.PlayClipAtPoint(pluginSFX, Camera.main.transform.position);

        flame.SetActive(false);

        hasBeenSnuffed = true;
    }
}