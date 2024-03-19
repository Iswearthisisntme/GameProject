using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.01f;

    Animator anim;
    float monsterYPos;

    void Start()
    {
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
            var PlayerHealth = player.GetComponent<PlayerHealth>();
            PlayerHealth.TakeDamage();
        }
    }
}
