using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
    public Transform interactableBox;
    public bool interactable, pickedup, isThrown;
    public Rigidbody rb;
    public float throwAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("InteractableBox"))
        {
            interactable = true;
            interactableBox = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableBox"))
        {
            if(pickedup == false)
            {
                interactable = false;
            }
            if (pickedup == true)
            {
                gameObject.transform.parent = null;
                rb.useGravity = true;
                interactable = false;
                pickedup = false;
            }
        }
    }
    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.parent = interactableBox;
                rb.useGravity = false;
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.transform.parent = null;
                rb.useGravity = true;
                pickedup = false;
            }
            if(pickedup == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    gameObject.transform.parent = null;
                    rb.useGravity = true;
                    rb.AddForce(interactableBox.forward * throwAmount,ForceMode.Impulse);
                    pickedup = false;
                }
            }
        }
    }

}
