using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Sword02 : MonoBehaviour
{
    Animator swordAnim;
    public Transform spamFile; // Skill effect bu game obje icin spamlancak cop kutusu
    public GameObject[] effectSkill;
    public int indexSkill = 0;
    public float comboTime01, comboTime02, comboTime03;


    private string[] skillNames;
    private Quaternion direction;

    private GameObject obj;

    VisualEffect visualEffect;

    void Start()
    {
        skillNames = new string[] { "SlashCombo", "Skill01Combo" };
        visualEffect = effectSkill[indexSkill].GetComponent<VisualEffect>();
        swordAnim = GetComponent<Animator>();
        StartCoroutine(SlashCombo());
    }
    private void Update()
    {
        //  PositionWord("PositionWord", transform.position);
        //print(transform.position);

    }

    // Slash Skill Combo
    IEnumerator SlashCombo()
    {

        while (true)
        {
            visualEffect = effectSkill[indexSkill].GetComponent<VisualEffect>();
            yield return new WaitForSeconds(2);
            swordAnim.Play(skillNames[indexSkill]);

            switch (indexSkill)
            {

                case 0:
                    yield return new WaitForSeconds(comboTime01); // 0.1f

                    direction = transform.rotation;
                    obj = Instantiate(effectSkill[indexSkill], transform.position, /*Quaternion.Euler(new Vector3(0, 90,180)*/ direction);
                    Destroy(obj, 1f);
                    yield return new WaitForSeconds(comboTime02); // 0.36f
                    direction = transform.rotation;

                    obj = Instantiate(effectSkill[indexSkill], transform.position,/* Quaternion.Euler(new Vector3(0, 90,0))*/direction);
                    Destroy(obj, 1f);

                    yield return new WaitForSeconds(comboTime03); // 0.45f
                    visualEffect.SetBool("ComboFirst", false);
                    visualEffect.SetBool("ComboLast", true);
                    direction = transform.rotation;


                    obj = Instantiate(effectSkill[indexSkill], transform.position,/* Quaternion.Euler(new Vector3(0, 90,0))*/direction);
                    Destroy(obj, 2.5f);


                    visualEffect.SetBool("ComboFirst", true);
                    visualEffect.SetBool("ComboLast", false);
                    break; // Slash Combo
                case 1:  // Skil01 Combo
                    Vector3 positionV3 = transform.position;
                   // StartCoroutine(PositionWord("PositionWord", positionV3));

                    direction = transform.rotation;
                    obj = Instantiate(effectSkill[indexSkill], transform.position, direction, transform);
                    Destroy(obj, 4f);

                    break;


            }


            yield return new WaitForSeconds(4.5f);
        }



    }
    
    // Visual Effect VFX  -> Bool deger control eder zaman icinde aktif eder
    //IEnumerator PositionWord(string name, Vector3 positionWord)
    //{

    //    visualEffect.SetVector3(name, positionWord); // Ýlk effect
    //    yield return new WaitForSeconds(1.2f);
    //  //  visualEffect.ResetOverride("Yarik Toprak Decal");
    //    visualEffect.SetVector3(name, positionWord); // Ýlk effect
    //}



}
