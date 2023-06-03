using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform obj;
    //Movement
    [SerializeField] private int speed = 5;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float ySpeed;
   // [SerializeField] private Rigidbody rb;
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
        Vector2 inputVec = new Vector2(horizontal, vertical);
        inputVec.Normalize();
        horizontal = inputVec.x;
        vertical = inputVec.y;



        Vector3 fref = findForwardvector();
        Vector3 rightref = findRightvector(fref);
       
        
        moveDirection = (vertical * fref) + (horizontal * rightref);
        
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

    }

    public Vector3 findForwardvector() {
        Vector3 fref;

        Vector3 camPosition = Camera.main.transform.position;
        Vector3 gibPosition = obj.position;

        
        


        fref = new Vector3(gibPosition.x - camPosition.x, 0, gibPosition.z - camPosition.z);
        fref.Normalize();

        return fref;
    }
    public Vector3 findRightvector(Vector3 fref)
    {
        Vector3 rightref = Vector3.Cross(fref, new Vector3(0, 1, 0));

        return -1 * rightref;
    }
}
