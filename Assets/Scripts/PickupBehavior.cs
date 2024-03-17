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
            transform.parent = other.transform;

            hasBeenCollected = true;

            gameObject.GetComponent<Animator>().SetTrigger("EndAnimation");

            //Destroy the pickup object
            //To be used when the inventory system is fully implemented
            //Destroy(gameObject);
        }
    }

    // Planned method to be implemented in the final game
    //private void OnDestroy()
    //{
    // Add nighlight icon to player inventory

    //}
}