using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.01f;

    Animator anim;
    float monsterYPos;

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

        monsterYPos = transform.position.y;

        anim = GetComponent<Animator>();

        anim.SetInteger("animState", 0);
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
            && hit.transform.tag == "MonsterUnderTheBed")
        {
            anim.SetInteger("animState", 0);
        }
        else 
        {
            transform.LookAt(player);
            anim.SetInteger("animState", 1);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            
        }
    }

    void OnCollisionEnter(Collision collision) 
    {   

       if (collision.gameObject.CompareTag("Block"))
        {
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody.velocity.magnitude > 0.3f) 
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition, 20 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 20 * Time.deltaTime);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            var PlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerHealth.TakeDamage();
        }
    }
}
