using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;
    

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractableBox"))
        {
            interactable = true;
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
                objTransform.parent = null;
                objRigidbody.useGravity = true;
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
                objTransform.parent = cameraTrans;
                objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
            if(pickedup == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    objTransform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                    pickedup = false;
                }
            }
        }
    }
}
