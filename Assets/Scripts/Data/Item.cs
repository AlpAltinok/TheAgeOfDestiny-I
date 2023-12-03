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
    PhysicalMinDamage,
    PhysicalMaxDamage,
    MagicalMinDamage,
    MagicalMaxDamage,
    MaxHealth,
    MaxMana,
    PhysicalDefence,
    MagicalDefence,
    ShotDistance,
    Heal,
}

public enum eItemType
{
    None,
    MainWeapon,
    SecondaryWeapon, //g
    DoubleWeapon, //g
    Armour, //Govde, Kask, Bileklik, Pantolon, Ayakkabi
    Rune, 
    Jewelry, //Kolye, Bilezik, Kupe, Kemer, Yuzuk, Rune
    Resource, //Craft yok,
    Potion, //Pot yok,
}

public enum Equipment 
{ 
    MainWeapon,
    SecondaryWeapon,
    Chest,
    Pant,
    Jewelry1,
    Jewelry2,
    Length, //bunun her zaman son element oldugundan emin olun, bu bize hizli yoldan ekipmanlarin kac cesit oldugu bilgisini vericek.
}

