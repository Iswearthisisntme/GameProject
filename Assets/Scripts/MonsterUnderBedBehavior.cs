using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUnderBedBehavior : MonoBehaviour
{
    Vector3 initialPosition;
    Quaternion initialRotation;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnTriggerEnter(Collider other) 
    {   
        if (other.gameObject.CompareTag("Block"))
        {
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody.velocity.magnitude > 0.3f) 
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition, 20 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 20 * Time.deltaTime);
            }
        }
    }
}
