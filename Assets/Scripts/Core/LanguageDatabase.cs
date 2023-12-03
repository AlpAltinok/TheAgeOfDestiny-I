using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageDatabase : MonoBehaviour
{
    public static LanguageDatabase instance { get; private set; }
    public LanguageData languageData;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        InstallLanguage();
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        ItemDatabase.instance.InstalItems();
    }

    void InstallLanguage()
    {
        //Burada daha sonra dil datalarini jsonlardan cekip mevcut dil ile degistirmis olucaz.
        //Headers
        languageData.Data = new List<TextData>();
        languageData.Data.Add(new TextData("Header_Stats", "Stats"));
        languageData.Data.Add(new TextData("Header_Weapons", "Weapons"));
        languageData.Data.Add(new TextData("Header_Armours", "Armours"));
        languageData.Data.Add(new TextData("Header_Jewelries", "Jewelries"));
        languageData.Data.Add(new TextData("Header_Runes", "Runes"));
        languageData.Data.Add(new TextData("Header_Skills", "Skills"));
        languageData.Data.Add(new TextData("Header_Quests", "Quests"));
        languageData.Data.Add(new TextData("Header_Crafts", "Resources"));
        languageData.Data.Add(new TextData("Header_Map", "Map"));
        languageData.Data.Add(new TextData("Header_Settings", "Settings"));
        languageData.Data.Add(new TextData("WeaponPanel_Header", "Weapons"));
        languageData.Data.Add(new TextData("WeaponPanel_TooltipHeader", "Tooltip"));
        languageData.Data.Add(new TextData("WeaponPanel_Tooltip_Equip", "Equip"));
        languageData.Data.Add(new TextData("WeaponPanel_Tooltip_Destroy", "Destroy"));
        languageData.Data.Add(new TextData("ArmourPanel_Header", "Armours"));
        languageData.Data.Add(new TextData("JevelriesPanel_Header", "Jewelries"));
        languageData.Data.Add(new TextData("Text_Tooltip_Equipped", "(Equipped*)"));
        languageData.Data.Add(new TextData("Text_Tooltip_Equip_Button", "Equip"));
        languageData.Data.Add(new TextData("Text_Tooltip_Unequip_Button", "Unequip"));

        languageData.Data.Add(new TextData("Item_1001", "Wood Sword"));
        languageData.Data.Add(new TextData("Item_1001_Desc", "This is a very simple wooden sword. Pretty useless."));
        languageData.Data.Add(new TextData("Item_1002", "Stone Sword"));
        languageData.Data.Add(new TextData("Item_1002_Desc", "This is a very simple stone sword. Pretty useless. But Better than wood"));
        languageData.Data.Add(new TextData("Item_1003", "Iron Sword"));
        languageData.Data.Add(new TextData("Item_1003_Desc", "This is a very simple iron sword. It is just okay."));
        languageData.Data.Add(new TextData("Item_2001", "Iron Armour"));
        languageData.Data.Add(new TextData("Item_2001_Desc", "This is an iron plate that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_2002", "Diamond Armour"));
        languageData.Data.Add(new TextData("Item_2002_Desc", "This is an diamond plate that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_2003", "Obsidian Armour"));
        languageData.Data.Add(new TextData("Item_2003_Desc", "This is an obsidian plate that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_3001", "Jewelry1"));
        languageData.Data.Add(new TextData("Item_3001_Desc", "This is an jewelry with low level."));
        languageData.Data.Add(new TextData("Item_3002", "Jewelry2"));
        languageData.Data.Add(new TextData("Item_3002_Desc", "This is an jewelry with middle level."));
        languageData.Data.Add(new TextData("Item_3003", "Jewelry3"));
        languageData.Data.Add(new TextData("Item_3003_Desc", "This is an jewelry with high level."));

        languageData.Data.Add(new TextData("Gold", "Gold"));

        LanguageTextData[] texts = Resources.FindObjectsOfTypeAll<LanguageTextData>();
        foreach (var data in languageData.Data)
        {
            foreach (var text in texts)
            {
                if (text.Key == data.uid)
                {
                    text.UpdateText(data.text);
                }
            }
        }
    }

    public string GetTextOf(string key)
    {
        return languageData.Data.Find(item => item.uid == key).text;
    }

    [System.Serializable]
    public class LanguageData 
    {
        public List<TextData> Data;
    }

    [System.Serializable]
    public class TextData
    {
        public string uid;
        public string text;

        public TextData(string _uid, string _text)
        {
            uid = _uid;
            text = _text;
        }
    }
}
