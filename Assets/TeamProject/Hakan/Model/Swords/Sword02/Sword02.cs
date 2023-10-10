using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Sword02 : MonoBehaviour
{
    Animator swordAnim;
    public GameObject Effect;
    VisualEffect visualEffect;
    // Start is called before the first frame update
    void Start()
    {
        visualEffect = Effect.GetComponent<VisualEffect>();
        swordAnim = GetComponent<Animator>();
        StartCoroutine(SwordActive());
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    IEnumerator SwordActive()
    {
        GameObject obj;
        //  Vector3 effectSlash;
      
       
        Quaternion direction;
        while (true)
        {
            yield return new WaitForSeconds(2);
            swordAnim.SetTrigger("Active");
            yield return new WaitForSeconds(0.1f);



            // effectSlash = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            direction = transform.rotation;
             obj = Instantiate(Effect, transform.position, /*Quaternion.Euler(new Vector3(0, 90,180)*/ direction);
            Destroy(obj,1f);
            yield return new WaitForSeconds(0.36f);
            direction = transform.rotation;
          //  effectSlash = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            obj = Instantiate(Effect, transform.position,/* Quaternion.Euler(new Vector3(0, 90,0))*/direction);
            Destroy(obj, 1f);

            yield return new WaitForSeconds(0.45f);
            visualEffect.SetBool("ComboFirst", false);
            visualEffect.SetBool("ComboLast", true);
            direction = transform.rotation;

         //   Time.timeScale = 0.4f;
            obj = Instantiate(Effect, transform.position,/* Quaternion.Euler(new Vector3(0, 90,0))*/direction);
            Destroy(obj, 1.5f);
            
            yield return new WaitForSeconds(0.5f);
          //  Time.timeScale = 1;
            visualEffect.SetBool("ComboFirst", true);
            visualEffect.SetBool("ComboLast", false);
        }

    }



}
