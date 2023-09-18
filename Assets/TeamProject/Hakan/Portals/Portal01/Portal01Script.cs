using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Portal01Script : MonoBehaviour
{
    public VisualEffect[] VFXportal01; //  Portal  0 index / PortalDown 1 index
  //  VisualEffect VFXportal01;

    bool activePortal = false;       // Portal aktif et yada devre dýsý yap
    float lifeTimeRate = 0.050f;     // portal aktif edilirken  ve kapalýrken  yavasca devreye sok

    private float ýnSpeed = -2f;
    public bool stop = false;    // Coroutine While durdur
    // Start is called before the first frame update
    void Start()
    {
      //  VFXportal01 = GetComponent<VisualEffect>();
    }

    // VFX play baslatmak 
    private void VfxEffectsPlay(VisualEffect[] effects)
    {
        foreach (var item in effects)
        {
            item.Play();
        }

    }
    private void VfxEffectActive(VisualEffect[] effects)
    {
        foreach (var item in effects)
        {
            item.SetBool("FirstActive", activePortal);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !activePortal)
        {
            stop = false;
            activePortal = true;
            //  VFXportal01.Play();
            VfxEffectsPlay(VFXportal01); // vfx play calýstýr
            StartCoroutine(PortalState());
        }
        else if (Input.GetKeyDown(KeyCode.Q)/* && activePortal*/)
        {
            stop = false;
            activePortal = false;
            StartCoroutine(PortalState());
        }
        VfxEffectActive(VFXportal01);
      // VFXportal01.SetBool("FirstActive", activePortal);
    }
    IEnumerator PortalState()
    {
        float time = VFXportal01[0].GetFloat("LifeTime"); // Portal Effectin ic kýsmýnýn sefaflýk degeri zaman göre art veya azal
        VFXportal01[0].SetBool("Active", false); // baslangýc effect devreye girsin  etrafa yayýlan parcacýk  Name -> (VFX Portal Particle ) 
        bool resultLimit; // sefaflýk degeri artarken true | azalýrken false calýsýr  



        while (!stop)
        {
            ýnSpeed = activePortal == true ? -2 : -5;
            resultLimit = activePortal == true ? time >= 1f : time <= 0f; // active edilirse 1  | devre dýsý 0 degeri alýr
            if (resultLimit)
            {
                // while() durdur 
                stop = true;
                StopCoroutine(PortalState());
            }

            time += activePortal == true ? lifeTimeRate : -lifeTimeRate; // portal active iken ++ | devre dýsý iken -- deger sayacý 

            VFXportal01[0].SetFloat("LifeTime", time);         // time degeri göre  sefaflýk degerleri  gösterir yada kapaltýr
          
            VFXportal01[0].SetFloat("InSpeed", ýnSpeed);
            yield return new WaitForSeconds(lifeTimeRate);

        }

    }
}
