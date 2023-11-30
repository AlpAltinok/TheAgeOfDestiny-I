using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerScript : MonoBehaviour
{
    private CharacterController controller;
    private Transform mainCamTransform;

    public float speed = 20f;
    public float gravity = 9.81f; // You can adjust the gravity value as needed
    private float verticalVelocity = 0.0f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        mainCamTransform = Camera.main.transform;
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Check if the character is grounded
        if (controller.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            // Apply gravity to the vertical velocity
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Apply the vertical velocity to move the character
        Vector3 moveDirection = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
