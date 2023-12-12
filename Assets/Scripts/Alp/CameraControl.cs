using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /* [Range(50, 500)]
     public float sens;

     public Transform body;
     float xRot = 0f;
     private void Start()
     {
         Cursor.visible = false;
     }
     private void Update()
     {
         Cursor.visible = false;
         float rotX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
         float rotY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;
         xRot -= rotY;
         xRot = Mathf.Clamp(xRot, -50f, 50f);
         transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

         body.Rotate(Vector3.up * rotX);
     }*/
    public float speed = 5f; // Karakterin hareket hýzý
    public float rotationSpeed = 300f; // Kamera döndürme hýzý

    void Update()
    {
        // Karakterin hareketi
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveAmount = moveDirection * speed * Time.deltaTime;
        transform.Translate(moveAmount);

        // Kamera döndürme
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 rotationAmount = new Vector3(0f, mouseX, 0f) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAmount);
    }
}
