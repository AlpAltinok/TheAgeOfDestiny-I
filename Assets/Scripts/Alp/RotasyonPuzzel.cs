using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Cinemachine;



public class RotasyonPuzzel : MonoBehaviour
{
    [SerializeField]
   Transform player;
   
    public float interactionDistance = 6f;
    public float rotationAmount = 5f;
    public float rotationDuration = 0.5f;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera followVirtualCamera;

    private bool interacting = false;
    

   

   
  
    private void Start()
    {
      
    }
  
    void Update()
    {
        if ( Vector3.Distance(transform.position, player.position) <= interactionDistance && Input.GetKey(KeyCode.E))
        {
            interacting = !interacting;
            //player.GetComponent<KarakterKontrol>().canMove = !interacting;
          
            PlayerMovement.Etkilesim = !interacting;
            followVirtualCamera.gameObject.SetActive(!interacting);
            virtualCamera.gameObject.SetActive(interacting);
           
          
        }

        if (interacting)
        {
              if (Input.GetKeyDown(KeyCode.A))
              {
                  transform.DORotate(new Vector3(0, -rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
              }
              else if (Input.GetKeyDown(KeyCode.D))
              {
                  transform.DORotate(new Vector3(0, rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
              }

           
            //transform.Rotate(new Vector3(0, axis) * Time.deltaTime * 10f);
        }
    }
  
 

}

