using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterMove : MonoBehaviour
{
    public float speed = 5f; // Karakterin hareket h�z�
    public float rotationSpeed = 300f; // Kamera d�nd�rme h�z�
    public float minYRotation = -80f; // Kameran�n minimum y ekseni rotasyonu
    public float maxYRotation = 80f; // Kameran�n maksimum y ekseni rotasyonu

    private float currentXRotation = 0f;

    void Update()
    {
        // Karakterin hareketi
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveAmount = moveDirection * speed * Time.deltaTime;
        transform.Translate(moveAmount);

        // Kamera d�nd�rme
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentXRotation -= mouseY * rotationSpeed * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, minYRotation, maxYRotation);

        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
    }
}