using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public List<Stat> BaseStats; //Herhangi bir ekipmanin base olarak kendisinin verdigi bonus listesi. Min atk max atk etc.
    public List<Stat> BonusStats; //Herhangi bir ekipmana daha sonradan oyuncu tarafindan eklenen extra statlar, Kritik, kanatma sansi, yere dusurme sansi etc.
    public int type; //Weapon, armour, rune, jewelry....
    public int currentAmount; //Oyuncu bu itemden kac adet tasiyor uzerinde.
    public int maxAmount; //1 slot icinde ayni itemden maksimum kac adet tasinabilir? ornegin potlar 1 den fazla iken, silahlar sloat basina 1 silah olarak tercih edilir.
    public float sellToShopPrice;
    public float buyFromShopPrice;
    public float score;
    public int Rarity;

    public Item(int _id, List<Stat> _BaseStats, List<Stat> _BonusStats, int _type, int _currentAmount, int _maxAmount, float _sellToShopPrice, float _buyFromShopPrice, float _score)
    {
        id = _id;
        BaseStats = InstallList(_BaseStats);
        BonusStats = InstallList(_BonusStats);
        type = _type;
        currentAmount = _currentAmount;
        maxAmount = _maxAmount;
        sellToShopPrice = _sellToShopPrice;
        buyFromShopPrice = _buyFromShopPrice;
        score = _score;
    }

    public Item(Item _item)
    {
        id = _item.id;
        BaseStats = InstallList(_item.BaseStats);
        BonusStats = InstallList(_item.BonusStats);
        type = _item.type;
        currentAmount = _item.currentAmount;
        maxAmount = _item.maxAmount;
        sellToShopPrice = _item.sellToShopPrice;
        buyFromShopPrice = _item.buyFromShopPrice;
        score = _item.score;
    }

    List<Stat> InstallList(List<Stat> _from)
    {
        List<Stat> _to = new List<Stat>();
        int length = _from.Count;
        for (int i = 0; i < length; i++)
            _to.Add(new Stat(_from[i]));
        return _to;
    }
}

[System.Serializable]
public class Stat
{
    public int id;
    public float amount;

    public Stat(int _id, float _amount)
    {
        id = _id;
        amount = _amount;
    }

    public Stat(Stat _stat)
    {
        id = _stat.id;
        amount = _stat.amount;
    }
}

public enum eStat
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

public enum eItemType
{
    None,
    MainWeapon,
    Chest,
    Helmet,
    wristlet,
    Pant,
    Boot,
    Necklace,
    Bracelet,
    Earring,
    Belt,
    Ring,
    Resource, //Craft yok,
    Potion, //Pot yok,
}

public enum Equipment 
{
    None,
    MainWeapon,
    Chest,
    Helmet,
    wristlet,
    Pant,
    Boot,
    Necklace,
    Bracelet,
    Earring,
    Belt,
    Ring,
    Length, //bunun her zaman son element oldugundan emin olun, bu bize hizli yoldan ekipmanlarin kac cesit oldugu bilgisini vericek.
}

