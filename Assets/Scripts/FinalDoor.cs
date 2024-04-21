using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{

    private GameObject player;
    // private GameObject door;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // door = GameObject.FindGameObjectWithTag("FinalDoor");
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetButtonDown("Fire1"))
    //     {
    //         float distance = Vector3.Distance(door.transform.position, player.transform.position);
    //         //print(distance);
    //         if (distance < 2.6f) //can adjust range later
    //         {
    //             levelManager.LevelWon();
    //             //AudioSource.PlayClipAtPoint(pluginSFX, Camera.main.transform.position);
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player was the colliding object with the pickup
        if (other.CompareTag("Player"))
        {
            levelManager.LevelWon();

            // transform.parent = other.transform;

            // //AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

            // gameObject.GetComponent<Animator>().SetTrigger("EndAnimation");

            // //Destroy the pickup object
            // //To be used when the inventory system is fully implemented
            // //Destroy(gameObject);
        }
    }
}
