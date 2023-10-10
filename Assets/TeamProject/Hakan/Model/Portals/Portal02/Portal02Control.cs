using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal02Control : MonoBehaviour
{
    Transform portalSpear, portalInActive;     // PortalSpear(Mýzrak) (Indext 0) ,PortalInActive (Indext 3)   
    Material PortalInMat;       // PortalIn(ic Effect) Shader Material
    public bool active = false;

    private float circleScale = 0.4f;        // Shader Material CircleScale(Sh_degiþkeni)  degeri Mafth.Lerp takip komutu
    [Header("Shader PortalIn Setting")]
    public float circleScaleSpeed = 0.4f;      // Shader Material CircleScale Mafth.Lerp Speed hýzý
    [Header("Shader PortalInActive Setting")]
    public float powerInActive = 6f;
    public float powerSpeed = 2.7f;
    public float timeActive = 2.3f;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ActivePortal());  //                                 ->> test
        portalSpear = transform.GetChild(1); // PortalSpear

        PortalInMat = transform.GetChild(2).GetComponent<MeshRenderer>().material;   // Portal In (Effect) Shader Material
        portalInActive = transform.GetChild(3); // PortalInActive  Ýc effect mýzrak hali olan 

    }

    // Update is called once per frame
    void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.Space)*/active)
        {
           // active = true;
            portalSpear.GetChild(0).gameObject.SetActive(active); // PortalSword Child Particle effect active
            portalSpear.GetComponent<Animator>().SetTrigger("Active");  // PortalSword Animasyon Tetikle

        }

        if (active)
        {
            ShaderMatControl(circleScale, circleScaleSpeed); // Shader material method control   ---->  geri koncak
            Invoke("PortalInActive", timeActive);  // PortalInActive (ic mýzrak effect belirli süre sonra aktif et)
        }

    }
    // PortalInActive (ic mýzrak effect belirli süre sonra aktif et)
    private void PortalInActive()
    {

        portalSpear.gameObject.SetActive(false); // Mýzrak Mesh Gizle
        portalInActive.gameObject.SetActive(true); // Portal ic mýzrak effect aktif et

        powerInActive = Mathf.MoveTowards(powerInActive, 0.4f, powerSpeed * Time.deltaTime);
        portalInActive.GetComponent<MeshRenderer>().material.SetFloat("_Power", powerInActive);

    }
    // Shader material icinde bütün degerleri control ediyorum
    private void ShaderMatControl(float circleScaleLimit, float circleScaleSpeed)
    {


        circleScale = Mathf.Lerp(circleScaleLimit, 0.4f, circleScaleSpeed * Time.deltaTime);
        PortalInMat.SetFloat("_CircleScale", circleScale);

    }

    // -> test
    IEnumerator ActivePortal() 
    {
        yield return new WaitForSeconds(3f);
        active = true;

    }

}
