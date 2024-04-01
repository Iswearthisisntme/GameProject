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

    float elapsedTime = 0;
    public float attackRate = 2.0f;

    public AudioClip monsterDamageSFX;


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
        elapsedTime += Time.deltaTime;
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

    void OnTriggerEnter(Collider other) 
    {   
        Debug.Log("enter");
       if (other.gameObject.CompareTag("Block"))
        {
        Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody.velocity.magnitude > 0.3f) 
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition, 20 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, 20 * Time.deltaTime);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player");
            var PlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if(elapsedTime >= attackRate)
            {
                Debug.Log("took damage");
                AudioSource.PlayClipAtPoint(monsterDamageSFX, Camera.main.transform.position);
                PlayerHealth.TakeDamage();
                elapsedTime = 0.0f;
            }
            
        }
    }
}
