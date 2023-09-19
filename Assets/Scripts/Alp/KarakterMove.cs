using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Karakterin yürüme hýzý
    public float jumpForce = 10.0f; // Zýplama kuvveti
    private bool isGrounded; // Karakterin yerde olup olmadýðýný kontrol etmek için kullanýlýr

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

            // Karakterin zýplama kontrolü
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        } 
     
    }

    // Karakter yerdeyken bu fonksiyon çaðrýlýr
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
