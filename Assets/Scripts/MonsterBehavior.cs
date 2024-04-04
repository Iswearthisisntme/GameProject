using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.01f;

    Animator anim;
    float monsterYPos;
    public float minDistance = 1;

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
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance > minDistance) 
        {
            FaceTarget(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect() 
    {
        //ideas if animation is too snappy:
        //angle check (is it within an approximate cone or angle)
        //add a threshold before you can change states
        // a time? (change every .5 seconds)
        //starts the state once you're within 10 degrees

        RaycastHit hit;
        float step = moveSpeed * Time.deltaTime;

        if(Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity)
            && hit.transform.tag == "MonsterUnderTheBed")
        {
            anim.SetInteger("animState", 0);
        }
        else 
        {
            FaceTarget(player.position);
            anim.SetInteger("animState", 1);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 
            10 * Time.deltaTime);
    }
}
