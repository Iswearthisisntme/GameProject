using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 6;
    public int numOfHearts;
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetButton("Jump"))
            {                
            TakeDamage();
            }

        for (int i = 0; i < hearts.Length; i++) {

            if (i < currentHealth) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts ) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage()
    {
        if(currentHealth > 0)
        {
            currentHealth -= 1;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        transform.Rotate(-90, 0, 0, Space.Self);
    }
}
