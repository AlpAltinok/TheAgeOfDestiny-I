using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossSkill01 : MonoBehaviour
{
    public GameObject VFX_BossSkill01;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(VFX_BossSkill01, transform.position, Quaternion.identity);
        }
    }
}
