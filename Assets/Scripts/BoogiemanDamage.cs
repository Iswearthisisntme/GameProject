using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoogiemanDamage : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;

    public int currentHealth;
    public float attackRate = 2.0f;
    public AudioClip monsterDamageSFX;

    float elapsedTime = 0;


    void Start()
    {
        currentHealth = startingHealth;
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
            if (elapsedTime >= attackRate)
            {
                int attackTimes = Random.Range(1, 2);

                AudioSource.PlayClipAtPoint(monsterDamageSFX, Camera.main.transform.position);
                for (int i = 0; i < attackTimes; i++)
                {
                    PlayerHealth.TakeDamage();
                    print(PlayerHealth.currentHealth);
                }
                elapsedTime = 0.0f;
            }

        }
    }
}
