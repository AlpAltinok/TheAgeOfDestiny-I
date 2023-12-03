using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public PlayerSaveData CurrentActiveSave;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (CurrentActiveSave.Equipments.Count < (int)Equipment.Length)
        {
            do
            {
                CurrentActiveSave.Equipments.Add(ItemDatabase.instance.GetItem(0));
            } while (CurrentActiveSave.Equipments.Count <= (int)Equipment.Length);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isLoadedGame())
                Save(CurrentActiveSave.SaveName, CurrentActiveSave.CreatedUID);
            else
                Save("TestNewSave", ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds());
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadSavedGame("TestNewSave", 1701407785);
        }
    }

    public void LoadSavedGame(string SaveName, long createdUID)
    {
        PlayerSaveData data = new PlayerSaveData();
        string _theSave = SaveName + "-" + createdUID;

        if (File.Exists(Application.persistentDataPath + "/" + _theSave + ".json"))
        {
            Debug.Log("CurrentSaveName Solo1: " + _theSave);
            CurrentActiveSave = LoadMyData(Application.persistentDataPath + "/" + _theSave + ".json");
            if (data != null)
            {
                Debug.Log("game is loaded succesffully.");
                if (CurrentActiveSave.Equipments.Count < (int)Equipment.Length)
                {
                    do
                    {
                        CurrentActiveSave.Equipments.Add(ItemDatabase.instance.GetItem(0));
                    } while (CurrentActiveSave.Equipments.Count >= (int)Equipment.Length);
                }
            }
        }
    }

    public PlayerSaveData LoadMyData(string pathToDataFile)
    {
        string jsonString = File.ReadAllText(pathToDataFile); // read the json file from the file system
        PlayerSaveData myData = JsonUtility.FromJson<PlayerSaveData>(jsonString); // de-serialize the data to your myData object
        return myData;
    }

    public bool isLoadedGame()
    {
        if (CurrentActiveSave == null)
            return false;
        else
        {
            if (CurrentActiveSave.SaveName == "")
                return false;
            else
                return true;
        }
    }

    public void Save(string saveName, long createdUID)
    {
        Debug.Log("Entered saveName: " + saveName +"-" + createdUID);
        string FileName = saveName + "-" + createdUID;
        Debug.Log("Filename: " + FileName);

        if (File.Exists(Application.persistentDataPath + "/" + FileName + ".json")) //Check the save name is exist
        {
            Debug.Log("This name of save is already exist. Save will overwrite.");
            File.Delete(Application.persistentDataPath + "/" + FileName + ".json");
        }
        else
        {
            Debug.Log("This name of save is free. Will create a new save file.");
        }
        
        long unixTimestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        PlayerSaveData savedata = new PlayerSaveData();
        savedata.SaveName = saveName;
        savedata.Gold = 0;
        savedata.CreatedUID = createdUID;
        savedata.LastSave = unixTimestamp;
        CurrentActiveSave = savedata;

        string jsonString = JsonUtility.ToJson(savedata); // this will give you the json (i.e serialize the data)

        File.WriteAllText(Application.persistentDataPath + "/" + savedata.SaveName + "-" + createdUID + ".json", jsonString); // this will write the json to the specified path
        Debug.Log("Game Save Location: " + Application.persistentDataPath + "/" + savedata.SaveName + "-" + createdUID + ".json");
    }

    [Serializable]
    public class PlayerSaveData
    {
        public string SaveName;
        public float Gold;
        public long CreatedUID;
        public long LastSave;

        public List<Item> Equipments = new List<Item>();
        public List<Item> Inventory = new List<Item>();
    }
}
