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

    public static bool etkilesim;
    void Start()
    {
        animator = GetComponent<Animator>();


        isWalkingHash = Animator.StringToHash("iswalking");
        isRunningHash = Animator.StringToHash("isrunning");

    }

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    //input system get data
    private void Awake()
    {

        input = new PlayerInput();
        input.PlayerMovement.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        input.PlayerMovement.run.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.PlayerMovement.Attack.started += ctx => Attack();
        input.PlayerMovement.Move.canceled += ctx =>
        {
            movementPressed = false;

        };
        input.PlayerMovement.Etkilesim.started += ctx => OnEtkilesim();
    }

    void Attack()
    {
        animator.SetTrigger("isattacking");

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    //karakter Animasyonu
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
    //karakter Haraketi
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
    public void OnDisable()
    {
        input.PlayerMovement.Disable();

    }
    public void OnEtkilesim()
    {
        etkilesim = true;
        input.PlayerMovement.Disable();
    }
}