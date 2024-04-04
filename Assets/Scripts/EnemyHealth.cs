using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    
    public int currentHealth;
    public float attackRate = 2.0f;
    public AudioClip monsterDamageSFX;


    Animator anim;
    float elapsedTime = 0;
    Vector3 initialPosition;
    Quaternion initialRotation;


    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount) 
    {
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
        
        if(currentHealth <= 0)
        {
            //dead!
            anim.SetInteger("animState", 0);
        }

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
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
