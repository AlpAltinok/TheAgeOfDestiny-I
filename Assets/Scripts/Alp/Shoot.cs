using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Mermi prefab'�
    public Transform firePoint; // Ate� noktas�
    public float projectileSpeed = 10f; // Mermi h�z�

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    void Shooting()
    {
        // Mermiyi olu�tur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Mermiyi ileri do�ru f�rlat
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(firePoint.forward * projectileSpeed, ForceMode.VelocityChange);
        }

        // Belirli bir s�re sonra mermiyi yok et
        Destroy(projectile, 2f);
    }
}
