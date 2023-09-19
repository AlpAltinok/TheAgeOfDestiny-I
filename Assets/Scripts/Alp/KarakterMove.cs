using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Karakterin y�r�me h�z�
    public float jumpForce = 10.0f; // Z�plama kuvveti
    private bool isGrounded; // Karakterin yerde olup olmad���n� kontrol etmek i�in kullan�l�r

    private Rigidbody rb;
    public static bool Move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Move = true;
    }

    void Update()
    {
        if (Move == true)
        {
            // Karakterin yatay hareketini al
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Karakteri yatay olarak hareket ettir
            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            Vector3 moveVelocity = moveDirection.normalized * moveSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

            // Karakterin z�plama kontrol�
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        } 
     
    }

    // Karakter yerdeyken bu fonksiyon �a�r�l�r
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
