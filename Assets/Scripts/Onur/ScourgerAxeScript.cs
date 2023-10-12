using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerAxeScript : MonoBehaviour
{
    public Transform axeTipTransform;

    [HideInInspector]
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isAttacking)
        {

        }
    }


}
