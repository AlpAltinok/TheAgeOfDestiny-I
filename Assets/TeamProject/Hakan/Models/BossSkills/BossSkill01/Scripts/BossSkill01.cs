using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill01 : MonoBehaviour
{
    public float speed,lifeTime;
    float time=0;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time<2f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
   
    }
       
}
