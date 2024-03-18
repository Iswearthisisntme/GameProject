using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.5f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect() 
    {
        RaycastHit hit;
        float step = moveSpeed * Time.deltaTime;

        if(Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity)
            && hit.transform.tag == "Monster")
        {
            
        }
        else 
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

     private void OnCollisionEnter(Collision collision)
    {   
        Debug.Log("kakak");
        if (collision.gameObject.CompareTag("Block"))
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, 20 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 20 * Time.deltaTime);
        }
        if(collision.gameObject.CompareTag("Player"))
        {   
            
            var PlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerHealth.TakeDamage();
        }
    }
}
