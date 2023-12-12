using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GecitAktif : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;

    public string targetTag = "TargetLayer";
    public float accelerationDuration = 1.5f;
    public float moveSpeed = 10f;
    public float startY = 0.2f;
    void OnCollisionEnter(Collision collision)
    {
        // Çarpýþan nesnenin layer'ýný kontrol et
        if ((targetLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            Debug.Log("calistim");
            SortObjectsInXAxis();
        }
    }

    void SortObjectsInXAxis()
    {
        // Find all objects with the target tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        // Sort the objects based on their index
        List<GameObject> sortedObjects = new List<GameObject>(objectsWithTag);
        sortedObjects.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        // Move and rotate each object
        for (int i = 0; i < sortedObjects.Count; i++)
        {
            GameObject obj = sortedObjects[i];

            // Calculate the destination position
            Vector3 destination = new Vector3(10f, startY * i, -26f - i);

            // Calculate the move duration based on moveSpeed and distance
            float moveDuration = Vector3.Distance(obj.transform.position, destination) / moveSpeed;

            // Use Dotween for movement with acceleration
            obj.transform.DOMove(destination, moveDuration)
                .SetEase(Ease.InOutQuad) // Ease function for acceleration
                .SetDelay(i * accelerationDuration) // Delay each object to create a staggered effect
                .OnStart(() => obj.transform.DORotate(Vector3.zero, moveDuration)); // Rotate to zero during the movement
                //.SetSpeedBased(); y ekseni hareketi hýzlý yapýyor/ amacý hýzý ayarlamak 
        }
    }
}
    
