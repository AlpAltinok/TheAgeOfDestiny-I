using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.InputSystem;


public class RotasyonPuzzel : MonoBehaviour
{
    public Transform player;
   
    public float interactionDistance = 6f;
    public float rotationAmount = 5f;
    public float rotationDuration = 0.5f;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera followVirtualCamera;
    private bool interacting = false;
    BulmacaInput input;

    private void Awake()
    {
     // input.SutunBulmaca.Rotasyon.performed += 
    }
    private void Start()
    {
      
    }
    public void RotasyonAktif() { 
    
    
    
    }
    void Update()
    {
        if ( Vector3.Distance(transform.position, player.position) <= interactionDistance && PlayerMovement.etkilesim==true)
        {
            interacting = !interacting;
            //player.GetComponent<KarakterKontrol>().canMove = !interacting;
            //player.GetComponent<PlayerMovement>().OnEtkilesim();
            PlayerMovement.etkilesim = !interacting;
            followVirtualCamera.gameObject.SetActive(!interacting);
            virtualCamera.gameObject.SetActive(interacting);
            Debug.Log("calýstý1");
        }

        if (interacting)
        {
            /*  if (Input.GetKeyDown(KeyCode.A))
              {
                  transform.DORotate(new Vector3(0, -rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
              }
              else if (Input.GetKeyDown(KeyCode.D))
              {
                  transform.DORotate(new Vector3(0, rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
              }*/

            Debug.Log("calýstý2");


        }
    }
    private void OnRotasyonSol()
    {
        transform.DORotate(new Vector3(0, -rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

    }
    private void OnRotasyonSag()
    {
        transform.DORotate(new Vector3(0, rotationAmount, 0), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

    }

}

