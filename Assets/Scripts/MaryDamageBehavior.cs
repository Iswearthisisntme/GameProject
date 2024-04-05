using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaryDamageBehavior : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    
    public int currentHealth;
    public float attackRate = 2.0f;
    public AudioClip monsterDamageSFX;


    Animator anim;
    float elapsedTime = 0;


    void Start()
    {
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

        if (other.gameObject.CompareTag("Player"))
        {
            var PlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if(elapsedTime >= attackRate)
            {
                int attackTimes = Random.Range(1,2);

                AudioSource.PlayClipAtPoint(monsterDamageSFX, Camera.main.transform.position);
                for (int i = 0; i < attackTimes; i++)
                {
                PlayerHealth.TakeDamage();
                }  
                elapsedTime = 0.0f;
            }
            
        }
    }
}
