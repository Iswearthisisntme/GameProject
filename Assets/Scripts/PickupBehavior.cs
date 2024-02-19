using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    private bool hasBeenCollected = false; // Flag to prevent double counting

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player was the colliding object with the pickup
        if (!hasBeenCollected && other.CompareTag("Player"))
        {
            // Destroy the pickup object
            Destroy(gameObject);
            hasBeenCollected = true;
        }
    }

    //private void OnDestroy()
    //{
        // TODO: Add nighlight icon to player inventory

    //}
}
