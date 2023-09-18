using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class xRayPuzzel : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;
    public LayerMask reflectiveLayerMask;
    public LayerMask doorLayerMask;
    public int rotationDuration = 2;
    private Coroutine doorOpenCoroutine;
    private RaycastHit doorHit;
    private float contactTime;
    private float contactThreshold = 3f;
    private bool doorOpened;

    void Update()
    {
        DrawReflectiveRay(transform.position, transform.forward, maxReflectionCount);
    }

    void DrawReflectiveRay(Vector3 position, Vector3 direction, int remainingReflections)
    {
        if (remainingReflections == 0) return;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxStepDistance, reflectiveLayerMask | doorLayerMask))
        {
            if (doorLayerMask == (doorLayerMask | (1 << hit.collider.gameObject.layer)))
            {
                doorHit = hit;
                Debug.DrawLine(position, hit.point, Color.red);
            }
            else
            {
                doorHit = new RaycastHit();

                Debug.DrawLine(position, hit.point, Color.red);

                Vector3 reflectedDirection = Vector3.Reflect(ray.direction, hit.normal);

                DrawReflectiveRay(hit.point + reflectedDirection * 0.01f, reflectedDirection, remainingReflections - 1);
            }
        }
        else
        {
            Debug.DrawRay(position, direction * maxStepDistance, Color.yellow);
            doorHit = new RaycastHit();
        }

        if (doorHit.collider != null && !doorOpened)
        {
            contactTime += Time.deltaTime;
            if (contactTime >= contactThreshold && doorOpenCoroutine == null)
            {
                Debug.Log("Kapý açýlýyor");
                doorOpenCoroutine = StartCoroutine(OpenDoorAfterDelay(doorHit.collider.gameObject.transform.parent, 0f, rotationDuration));
            }
        }
        else
        {
            contactTime = 0f;
        }
    }

    private IEnumerator OpenDoorAfterDelay(Transform doorParent, float delay, float rotationDuration)
    {
        yield return new WaitForSeconds(delay);
        doorParent.DORotate(new Vector3(0, 90, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        doorOpenCoroutine = null;
        doorOpened = true;
    }
}
