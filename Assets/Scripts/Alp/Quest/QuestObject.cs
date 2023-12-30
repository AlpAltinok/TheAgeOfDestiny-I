using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    private bool inTrigger = false;
    public List<int> availableQuestIDs = new List<int>(); // mevcut g�revler
    public List<int> receivableQuestIDs = new List<int>(); // al�nabilir g�revler
    void Update()
    {
        if(inTrigger==true && Input.GetKeyDown(KeyCode.E))
        {
            // u� questManager 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
