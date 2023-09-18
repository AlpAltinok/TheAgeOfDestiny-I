using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.InputSystem;


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
    PuzzleInput input;

   

    float axis;
    private void Awake()
    {
        input=new PuzzleInput();

        input.PuzzleMove.Exit.started += ctx => ExitCode();

        input.PuzzleMove.Move.performed += ctx => 
        {
            axis=ctx.ReadValue<float>();
            Debug.Log("axþs " + axis);

        };
        input.PuzzleMove.Move.canceled += ctx =>
        {
            axis = 0;
        };


    }
    private void Start()
    {
      
    }
   void ExitCode()
    {
        followVirtualCamera.gameObject.SetActive(interacting);
        virtualCamera.gameObject.SetActive(!interacting);
        PlayerMovement.etki = false;
        PlayerMovement.etkilesim = false;
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
            transform.Rotate(new Vector3(0, axis) * Time.deltaTime * 10f);
        }
    }
  
    private void OnEnable()
    {
        input.PuzzleMove.Enable();
    }
    private void OnDisable()
    {
            input.PuzzleMove.Disable();
    }


}

