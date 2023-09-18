using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Portal01Script : MonoBehaviour
{
    public VisualEffect[] VFXportal01; //  Portal  0 index / PortalDown 1 index
  //  VisualEffect VFXportal01;

    bool activePortal = false;       // Portal aktif et yada devre d�s� yap
    float lifeTimeRate = 0.050f;     // portal aktif edilirken  ve kapal�rken  yavasca devreye sok

    private float �nSpeed = -2f;
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
            VfxEffectsPlay(VFXportal01); // vfx play cal�st�r
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
        float time = VFXportal01[0].GetFloat("LifeTime"); // Portal Effectin ic k�sm�n�n sefafl�k degeri zaman g�re art veya azal
        VFXportal01[0].SetBool("Active", false); // baslang�c effect devreye girsin  etrafa yay�lan parcac�k  Name -> (VFX Portal Particle ) 
        bool resultLimit; // sefafl�k degeri artarken true | azal�rken false cal�s�r  



        while (!stop)
        {
            �nSpeed = activePortal == true ? -2 : -5;
            resultLimit = activePortal == true ? time >= 1f : time <= 0f; // active edilirse 1  | devre d�s� 0 degeri al�r
            if (resultLimit)
            {
                // while() durdur 
                stop = true;
                StopCoroutine(PortalState());
            }

            time += activePortal == true ? lifeTimeRate : -lifeTimeRate; // portal active iken ++ | devre d�s� iken -- deger sayac� 

            VFXportal01[0].SetFloat("LifeTime", time);         // time degeri g�re  sefafl�k degerleri  g�sterir yada kapalt�r
          
            VFXportal01[0].SetFloat("InSpeed", �nSpeed);
            yield return new WaitForSeconds(lifeTimeRate);

        }

    }
}
