using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public List<Quest> questList = new List<Quest>();       //Master quest list
    public List<Quest> currentQuestList = new List<Quest>(); // current quest list

    private void Awake()
    {
        if(questManager== null)
        {
            questManager = this;
        }
        else if (questManager!= this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
     
    }


    //Görev isteði
    public  void QuestRequest(QuestObject NPCQuestObject) 
    {
        //availble quest
        if (NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for(int i = 0;i < questList.Count; i++)
            {
                for(int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        Debug.Log("Quest ID: " + NPCQuestObject.availableQuestIDs[j] + " " + questList[i].progress);

                        AcceptQuest(NPCQuestObject.availableQuestIDs[j]);
                        // quest uý
                    }
                }
            }
        }
        // active quest

    }

    //Accept Quest
    public void  AcceptQuest(int questID)
    {
        for( int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
            }
        }
    }

    //COMPLETE Quest
    public void CompleteQeust(int questID)
    {
        for(int i=0; i < currentQuestList.Count; i++)
        {
            if(currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }

    //Görevde toplanacak item , öldürülecek düþman sayýsý tutar.
    public void AddQuestItem(string questObjective,int itemAmount)
    {
        for(int i= 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }
            if (currentQuestList[i].questObjectiveCount>=currentQuestList[i].questObjectiveRequirement&& currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }

    //BOOLS Görev kontrol
    public  bool  RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id==questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }
}
