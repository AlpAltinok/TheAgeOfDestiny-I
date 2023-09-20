using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    [SerializeField]
    float jumpForce;

    Vector3 MoveDirection;
    [SerializeField]
    float moveSpeed;
    float xDirection;
    float zDirection;


    int isWalkingHash;
    int isRunningHash;
    bool movementPressed;
    bool runPressed;

    public static bool Etkilesim;

    private void Start()
    {
        Etkilesim = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("iswalking");
        isRunningHash = Animator.StringToHash("isrunning");
    }
    private void Update()
    {
        movementPressed = xDirection != 0 || zDirection != 0;
        HandleMovement();
        HandleRotation();
        KeyDown();

    }
    void KeyDown()
    {

        if(Input.GetKey(KeyCode.LeftShift))
        {
            runPressed = true;
        }else runPressed = false;

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("isattacking");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 5f, 0)*jumpForce,ForceMode.VelocityChange);
            animator.SetTrigger("isjumping");
        }
    }

    private void Awake()
    {


    }

    void HandleMovement()
    {
        if (Etkilesim == true)
        {
            xDirection = Input.GetAxis("Horizontal");
            zDirection = Input.GetAxis("Vertical");


            MoveDirection = new Vector3(xDirection, 0, zDirection);
            transform.position += MoveDirection * moveSpeed * Time.deltaTime;

            bool isRunning = animator.GetBool(isRunningHash);
            bool isWalking = animator.GetBool(isWalkingHash);

            if (movementPressed && !isWalking)
            {
                animator.SetBool(isWalkingHash, true);

            }
            if (!movementPressed && isWalking)
            {
                animator.SetBool(isWalkingHash, false);
            }

            if ((movementPressed && runPressed) && !isRunning)
            {
                animator.SetBool(isRunningHash, true);
            }

            if ((!movementPressed || !runPressed) && isRunning)
            {
                animator.SetBool(isRunningHash, false);
            }
        }

    }
    void HandleRotation()
    {
        Vector3 currentpos = transform.position;
        Vector3 newpos = new Vector3(xDirection, 0, zDirection);
        Vector3 positionlookat = currentpos + newpos;
        Quaternion targetRotation = Quaternion.LookRotation(positionlookat - transform.position);


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

    }

    
}
