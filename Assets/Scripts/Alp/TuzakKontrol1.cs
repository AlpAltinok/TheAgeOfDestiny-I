using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TuzakKontrol1 : MonoBehaviour
{
   
    [SerializeField] LayerMask targetLayer;
    public List<GameObject> closedSteps; // Etkinle�tirilecek k�plerin listesi
    public float activationDelay = 0.5f;
    public float moveDistance = 3.0f; // Hareket mesafesi
    public float moveDuration = 1.0f;
    private bool isMoving = false;

    void OnCollisionEnter(Collision collision)
    {
        // �arp��an nesnenin layer'�n� kontrol et
        if ((targetLayer.value & 1 << collision.gameObject.layer) != 0 && !isMoving)
        {
            Debug.Log("calistim");
            StartCoroutine(ActivateAndMoveSteps());
        }
    }

    IEnumerator ActivateAndMoveSteps()
    {
        isMoving = true;

        for (int i = 0; i < closedSteps.Count; i++)
        {
            GameObject currentStep = closedSteps[i];

            // DOTween kullanarak basama�� a� ve belirli bir gecikmeyle
            Sequence sequence = DOTween.Sequence();

            // �lk konumunu belirle (a�a��da)
            Vector3 initialPosition = currentStep.transform.position;
            currentStep.transform.position = new Vector3(initialPosition.x, initialPosition.y - moveDistance, initialPosition.z);

            sequence.Append(currentStep.transform.DOMove(initialPosition, moveDuration)) // Hedef konumda hareket
                    .OnStart(() => currentStep.SetActive(true)) // Basama�� etkinle�tir
                    .AppendInterval(activationDelay); // Gecikme ekle

            yield return sequence.WaitForCompletion();
        }

        isMoving = false;
    }
}
