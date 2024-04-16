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

    float fastSpeed;

    CharacterController controller;
    Vector3 input, moveDirection;
    Animator anim;
    float jumpTransitionTimer = 0f;
    bool isJumping = false;
    float moveHorizontal;
    float moveVertical;
    float slowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        slowSpeed = moveSpeed / 100.0f;
        fastSpeed = moveSpeed * 1.8f;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("leftshit");
            input *= moveSpeed;
        } 
            Debug.Log("not");
            input *= fastSpeed;
        

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
        if (Input.GetKey(KeyCode.LeftShift))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Clutter"))
        {
            print("Clutter trigger");
            input *= slowSpeed;
            print(input);
            Invoke("ReturnToNormalSpeed", 3);
        }
    }

    private void ReturnToNormalSpeed()
    {
        print("normal speed");
        input *= moveSpeed;
    }

}
