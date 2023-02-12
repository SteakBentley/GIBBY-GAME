using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Movement
    [SerializeField] private int speed = 5;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float ySpeed;
    //Animation
    private Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        //Get horizontal and vertical inputs (usually WASD or Arrow Keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection *= speed;

        //rotation
        if (moveDirection != Vector3.zero)
        {

            
           Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
           transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // jumping
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * speed;
        ySpeed += Physics.gravity.y * Time.deltaTime;

            if(Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }

        Vector3 velocity = moveDirection;
        velocity.y = ySpeed/15;
        velocity.x = 0;
        velocity.z = 0;
        controller.Move(moveDirection * Time.deltaTime + velocity);
        //animation
        if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0f);
        }
        else
            animator.SetFloat("Speed", 1f);

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetFloat("Jumping", 0f);
        }
        else
            animator.SetFloat("Jumping", 1f);
    }
}
