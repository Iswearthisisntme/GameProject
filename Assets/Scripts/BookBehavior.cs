using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{

    public static bool isBookOpen = false;

    public GameObject book;

    public GameObject openBookInstructions;

    public GameObject player;

    public float minDistance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(book.transform.position, player.transform.position);
            if (distance < minDistance && !isBookOpen)
            {
                isBookOpen = true;
                Time.timeScale = 0f;
                openBookInstructions.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isBookOpen)
        {
            isBookOpen = false; 
            Time.timeScale = 1f;
            openBookInstructions.SetActive(false);
        }

    }
}
