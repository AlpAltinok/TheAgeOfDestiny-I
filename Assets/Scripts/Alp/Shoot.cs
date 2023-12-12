using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Mermi prefab'ý
    public Transform firePoint; // Ateþ noktasý
    public float projectileSpeed = 10f; // Mermi hýzý

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    void Shooting()
    {
        // Mermiyi oluþtur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Mermiyi ileri doðru fýrlat
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            projectileRb.AddForce(firePoint.forward * projectileSpeed, ForceMode.VelocityChange);
        }

        // Belirli bir süre sonra mermiyi yok et
        Destroy(projectile, 2f);
    }
}
