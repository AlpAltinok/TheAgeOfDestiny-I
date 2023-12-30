using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{

    public enum QuestProgress { NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE }

    public string title;                     // title for the quest
    public int id;                          // ID number for the quest
    public QuestProgress progress;          // state of the current quest
    public string description;              // string from our quest Giver
    public string hint;                     // ipucu
    public string congratulation;              
    public string summary;                  // özet
    public int nextQuest;                   // the next quest

    public string questObjective;            //name of the quest objective
    public int questObjectiveCount;          // current number of questobject
    public int questObjectiveRequirement;       // requried amount of quest

    public int goldReward;
    public int runReward;
    public string itemReward;
}
