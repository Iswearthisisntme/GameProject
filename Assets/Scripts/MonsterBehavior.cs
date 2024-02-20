using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.5f;

    void Start()
    {
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

        if(Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity))
        {
            
        }
        else 
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            var PlayerHealth = other.GetComponent<PlayerHealth>();
            PlayerHealth.TakeDamage();
        }
    }
}
