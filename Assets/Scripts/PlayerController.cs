using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float jumpHeight = 5.0f;
    public float gravity = 9.81f;
    public float airControl = 10;
    public float jumpTransitionDelay = 0.5f;
    public float sprintDuration = 1.0f;
    float originalSprintDuration;
    public float sprintCooldowns = 0.0f;
    float originalSprintCooldown = 3.0f;
    float noCooldown = 0.0f;

    float fastSpeed;
    bool isRunning;

    CharacterController controller;
    Vector3 input, moveDirection;
    Animator anim;
    float jumpTransitionTimer = 0f;
    bool isJumping = false;
    float moveHorizontal;
    float moveVertical;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        fastSpeed = moveSpeed * 1.8f;
        originalSprintDuration = sprintDuration;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        Debug.Log(sprintCooldowns);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(sprintCooldowns <= 0) 
            {
                input *= fastSpeed;
                isRunning = true;
                sprintDuration -= Time.deltaTime;
                Debug.Log(sprintDuration);
                if (sprintDuration <= 0)
                {
                    isRunning = false;
                    Debug.Log("sprint over");
                    anim.SetInteger("animState", 4);
                    input *= moveSpeed;
                    sprintDuration = originalSprintDuration;
                    sprintCooldowns = originalSprintCooldown;
                }
            }
            else
            {
                sprintCooldowns -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("not");
            input *= moveSpeed;
            sprintDuration = originalSprintDuration;
            sprintCooldowns = noCooldown;
            isRunning = false;
        }
        

        if (controller.isGrounded)
        {
            moveDirection = input;

            if (Input.GetButton("Jump"))
            {
                anim.SetInteger("animState", 1);
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                isJumping = true;
                jumpTransitionTimer = 0f;
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        //move the character down to account for gravity 
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        if (isJumping)
        {
            jumpTransitionTimer += Time.deltaTime;
            if (jumpTransitionTimer >= jumpTransitionDelay)
            {
                anim.SetInteger("animState", 0);
                isJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isRunning)
        {
            anim.SetInteger("animState", 3);
        }
        else 
        {
            if (moveHorizontal != 0 || moveVertical > 0)
            {
                anim.SetInteger("animState", 4);
            }
            else if (moveVertical < 0)
            {
                anim.SetInteger("animState", 5);
            }
            else if (!isJumping)
            {
                anim.SetInteger("animState", 0);
            }
        }
    }
    
}
