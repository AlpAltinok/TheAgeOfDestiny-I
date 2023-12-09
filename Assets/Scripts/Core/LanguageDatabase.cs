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
        languageData.Data.Add(new TextData("StatPanel_Header1", "Offensive"));
        languageData.Data.Add(new TextData("StatPanel_Header2", "Defensive"));

        languageData.Data.Add(new TextData("Gold", "Gold"));

        //Items
        AddItems();

        //Stats
        AddStats();

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

    public string GetSymbolOfStat(int _statID)
    {
        List<int> percentStats = new List<int>()
        {
            (int)eStat.DamgePercent,
            (int)eStat.MaxHealthPercent,
            (int)eStat.MaxManaPercent,
            (int)eStat.PhysicalDefencePercent,
            (int)eStat.MagicalDefencePercent,
            (int)eStat.HealAmountPercent,
            (int)eStat.CritDamage,
            (int)eStat.CritRate,
            (int)eStat.StaminaPercent,
            (int)eStat.Accuracy,
            (int)eStat.Evasion,
            (int)eStat.AttackSpeed,
            (int)eStat.ShotDistance,
            (int)eStat.HealthRegenerationPercent,
            (int)eStat.ManaRegenerationPercent,
            (int)eStat.BurnChance,
            (int)eStat.FreezeChance,
            (int)eStat.PoisonChance,
            (int)eStat.BurnRessist,
            (int)eStat.FreezeRessist,
            (int)eStat.PoisonRessist,
            (int)eStat.GoldFind
        };

        if (percentStats.Contains(_statID))
            return "%";
        else
            return "";
    }


    enum Enummm
    {
    None, 
    Damage, //+ hasar + bazli
    DamgePercent, //% hasar % bazli
    MaxHealth, //+ mana % bazli
    MaxHealthPercent, //% hp %bazli
    MaxMana, //+ mana + bazli
    MaxManaPercent, //% mana % bazli
    PhysicalDefence, //+ Fiziksel defans artisi + bazli
    PhysicalDefencePercent, //% Fiziksel defans artisi % bazli
    MagicalDefence, //+ Sihirli defans artisi + bazli
    MagicalDefencePercent, //% Sihirli defans artisi % bazli
    HealAmount, //+ Canavar oldurmelerden ekstra can kazanimi + bazli
    HealAmountPercent, //% Canavar oldurmelerden ekstra can kazanimi % bazli
    CritDamage, //% critical vuruslarin vuracagi fazladan hasar miktari
    CritRate, //% critical vurus sansi
    Stamina, //+ Kosma, ve savasma enerjisi artisi +
    StaminaPercent, //% Kosma, ve savasma enerjisi artisi %
    Accuracy, //% Dodge olabilecek vuruslari kirma sansi
    Evasion, //% Vuruslari dodgelama sansi
    AttackSpeed, //% Saldiri hizi
    //LifeSteal, //% oldurmelerden, hpnin % oranli geri kazanimi
    //ManaSteal, //% oldurmelerden, mananin % oranli geri kazanimi
    ShotDistance, //+ Metre bazli, okun ulasabilecegi maksimum menzili arttiran bir stat.
    HealthRegeneration, //Her 8 saniyede bir geri kazanilan hp miktari +olarak
    HealthRegenerationPercent, //Her 8 saniyede bir geri kazanilan hp miktari %olarak
    ManaRegeneration, //Her 8 saniyede bir geri kazanilan mana miktari +olarak
    ManaRegenerationPercent, //Her 8 saniyede bir geri kazanilan mana miktari %olarak
    BurnChance, //% Dusmanin her saniye bir miktar yanma hasari almasi icin sans
    FreezeChance, //% Dusmani dondurma sansi
    PoisonChance, //% Dusmanin her 3 saniyede bir yanamya oranla daha yuksek bir zehir hasari almasi icin sans
    BurnRessist,
    FreezeRessist,
    PoisonRessist,
    GoldFind, //% extra gold
    Length, //bunun her zaman son element oldugundan emin olun, bu bize hizli yoldan ekipmanlarin kac cesit oldugu bilgisini vericek.
}



public string GetTextOf(string key)
    {
        return languageData.Data.Find(item => item.uid == key).text;
    }

    void AddStats()
    {
        languageData.Data.Add(new TextData("Stat0", "Empty"));
        languageData.Data.Add(new TextData("Stat1", "Damage"));
        languageData.Data.Add(new TextData("Stat2", "Damage"));
        languageData.Data.Add(new TextData("Stat3", "Max Health"));
        languageData.Data.Add(new TextData("Stat4", "Max Health"));
        languageData.Data.Add(new TextData("Stat5", "Max Mana"));
        languageData.Data.Add(new TextData("Stat6", "Max Mana"));
        languageData.Data.Add(new TextData("Stat7", "Phy. Defence"));
        languageData.Data.Add(new TextData("Stat8", "Phy. Defence"));
        languageData.Data.Add(new TextData("Stat9", "Mgc. Defence"));
        languageData.Data.Add(new TextData("Stat10", "Mgc. Defence"));
        languageData.Data.Add(new TextData("Stat11", "Heal Amount"));
        languageData.Data.Add(new TextData("Stat12", "Heal Amount"));
        languageData.Data.Add(new TextData("Stat13", "Crit Damage"));
        languageData.Data.Add(new TextData("Stat14", "Crit Rate"));
        languageData.Data.Add(new TextData("Stat15", "Stamina"));
        languageData.Data.Add(new TextData("Stat16", "Stamina"));
        languageData.Data.Add(new TextData("Stat17", "Accuracy"));
        languageData.Data.Add(new TextData("Stat18", "Evasion"));
        languageData.Data.Add(new TextData("Stat19", "AttackSpeed"));
        languageData.Data.Add(new TextData("Stat20", "Shot Distance"));
        languageData.Data.Add(new TextData("Stat21", "Health Generation"));
        languageData.Data.Add(new TextData("Stat22", "Health Generation"));
        languageData.Data.Add(new TextData("Stat23", "Mana Generation"));
        languageData.Data.Add(new TextData("Stat24", "Mana Generation"));
        languageData.Data.Add(new TextData("Stat25", "Burn Chance"));
        languageData.Data.Add(new TextData("Stat26", "Freeze Chance"));
        languageData.Data.Add(new TextData("Stat27", "Poison Chance"));
        languageData.Data.Add(new TextData("Stat28", "Burn Ressist"));
        languageData.Data.Add(new TextData("Stat29", "Freeze Ressist"));
        languageData.Data.Add(new TextData("Stat30", "Poison Ressist"));
        languageData.Data.Add(new TextData("Stat31", "Gold Find"));
    }

    void AddItems()
    {
        languageData.Data.Add(new TextData("Item_1001", "Wood Sword"));
        languageData.Data.Add(new TextData("Item_1001_Desc", "This is a very simple wooden sword. Pretty useless."));
        languageData.Data.Add(new TextData("Item_1002", "Stone Sword"));
        languageData.Data.Add(new TextData("Item_1002_Desc", "This is a very simple stone sword. Pretty useless. But Better than wood"));
        languageData.Data.Add(new TextData("Item_1003", "Iron Sword"));
        languageData.Data.Add(new TextData("Item_1003_Desc", "This is a very simple iron sword. It is just okay."));

        languageData.Data.Add(new TextData("Item_2001", "Test Chest"));
        languageData.Data.Add(new TextData("Item_2001_Desc", "This is a Test Chest that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_3001", "Test Pant"));
        languageData.Data.Add(new TextData("Item_3001_Desc", "This is a Test Pant that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_4001", "Test Helmet"));
        languageData.Data.Add(new TextData("Item_4001_Desc", "This is a Test Helmet that prepared to save you from deadly hits."));
        languageData.Data.Add(new TextData("Item_5001", "Test Wristlet"));
        languageData.Data.Add(new TextData("Item_5001_Desc", "This is a Test Wristlet."));
        languageData.Data.Add(new TextData("Item_6001", "Test Boots"));
        languageData.Data.Add(new TextData("Item_6001_Desc", "This is a Test Boots."));

        languageData.Data.Add(new TextData("Item_7001", "Test Necklace"));
        languageData.Data.Add(new TextData("Item_7001_Desc", "This is an jewelry Test Necklace."));
        languageData.Data.Add(new TextData("Item_8001", "Test Earring"));
        languageData.Data.Add(new TextData("Item_8001_Desc", "This is an jewelry Test Earring."));
        languageData.Data.Add(new TextData("Item_9001", "Test Ring"));
        languageData.Data.Add(new TextData("Item_9001_Desc", "This is an jewelry Test Ring."));
        languageData.Data.Add(new TextData("Item_10001", "Test Bracelet"));
        languageData.Data.Add(new TextData("Item_10001_Desc", "This is an jewelry Test Bracelet."));
        languageData.Data.Add(new TextData("Item_11001", "Test Belt"));
        languageData.Data.Add(new TextData("Item_11001_Desc", "This is an jewelry Test Belt."));
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
