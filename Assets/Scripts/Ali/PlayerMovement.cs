using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();


        isWalkingHash = Animator.StringToHash("iswalking");
        isRunningHash = Animator.StringToHash("isrunning");

    }

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        input = new PlayerInput();
        input.PlayerMovement.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.PlayerMovement.run.performed += ctx => runPressed = ctx.ReadValueAsButton();

        input.PlayerMovement.Move.canceled += ctx =>
        {
            movementPressed = false;

        };
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {

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
    void HandleRotation()
    {
        Vector3 currentpos = transform.position;

        Vector3 newpos = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionlookat = currentpos + newpos;

        Quaternion targetRotation = Quaternion.LookRotation(positionlookat - transform.position);


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

    }
    private void OnEnable()
    {
        input.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        input.PlayerMovement.Disable();

    }
}
