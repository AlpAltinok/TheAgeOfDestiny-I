using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance { get; private set; }
    public List<Item> items = new List<Item>();
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

    public Item GetItem(int id)
    {
        Item foundItem = items.Find(item => item.id == id);
        return foundItem;
    }

    public void InstalItems()
    {
        items.Clear();

        Item emptyItem = new Item(0, //itemin idsi unique olmali.
            new List<Stat> //Itemin sahip olacagi statlar, combatta veya diger unsurlarda kullanilacak
            {
                //empty item de stat yok, diger orneklere bakabilirsiniz.
            }, 
            new List<Stat>(), //Core itemlerde bonus stat listesi, ilerde oyuncunun stat ekleyebilmesi icin bos olarak olusturulucak.
            (int)eItemType.None, //her itemin islevselligini kontrol edebilmek adina bunlar icin type tanimlandi.
            0, //Oyuncu kac adet tasiyor.
            0, //Oyuncu kac adet tasiyabilir maksimum.
            0, //Itemi saticiya satarken ne kadar kazanacak oyuncu?
            0, //Itemi saticidan ne kadara alacak oyuncu? Bu degerleri anlik runtimeda da guncelleyebilirsiniz elbette. Burada sadece base olarak giriyoruz.
            0 //Score degeri, itemleri siralamak ve kiyaslamak icin kullanacagimiz bir deger, scoru yuksek olan item, envanterde en ustte bulunacak. + ve bonus basma gibi faktorler score etkileyecek.
            );

        items.Add(emptyItem); //Databasede ilk item daima bos item olmali, bos bir item yaratmak icin 0 indexli itemi kullanicaz (ihtiyac durumunda).
        CreateWeapons();
        CreateArmours();
        CreateJewelries();
        CreateRunes();
        CreateResources();
        CreatePotions();
    }

    void CreateWeapons()
    {
        Item Weapon1 = new Item(1001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalMinDamage, 20),
                new Stat((int)eStat.PhysicalMaxDamage, 35)
            },
            new List<Stat>(), (int)eItemType.MainWeapon, 0, 1, 40, 10, 10);

        Item Weapon2 = new Item(1002,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalMinDamage, 29),
                new Stat((int)eStat.PhysicalMaxDamage, 43)
            },
            new List<Stat>(), (int)eItemType.MainWeapon, 0, 1, 100, 30, 15);

        Item Weapon3 = new Item(1003,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalMinDamage, 38),
                new Stat((int)eStat.PhysicalMaxDamage, 55)
            },
            new List<Stat>(), (int)eItemType.MainWeapon, 0, 1, 180, 60, 20);

        items.Add(Weapon1);
        items.Add(Weapon2);
        items.Add(Weapon3);
    }

    void CreateArmours()
    {
        Item Armour1 = new Item(2001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 12),
                new Stat((int)eStat.MagicalDefence, 7)
            },
            new List<Stat>(), (int)eItemType.Armour, 0, 1, 20, 5, 10);

        Item Armour2 = new Item(2002,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 19),
                new Stat((int)eStat.MagicalDefence, 12)
            },
            new List<Stat>(), (int)eItemType.Armour, 0, 1, 60, 15, 15);

        Item Armour3 = new Item(2003,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 29),
                new Stat((int)eStat.MagicalDefence, 18)
            },
            new List<Stat>(), (int)eItemType.Armour, 0, 1, 110, 25, 20);

        items.Add(Armour1);
        items.Add(Armour2);
        items.Add(Armour3);
    }

    void CreateJewelries()
    {
        Item Jewelry1 = new Item(3001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 40),
                new Stat((int)eStat.MaxMana, 15)
            },
            new List<Stat>(), (int)eItemType.Jewelry, 0, 1, 120, 50, 10);

        Item Jewelry2 = new Item(3002,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 60),
                new Stat((int)eStat.MaxMana, 25)
            },
            new List<Stat>(), (int)eItemType.Jewelry, 0, 1, 210, 100, 15);

        Item Jewelry3 = new Item(3003,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 90),
                new Stat((int)eStat.MaxMana, 40)
            },
            new List<Stat>(), (int)eItemType.Jewelry, 0, 1, 340, 150, 20);

        items.Add(Jewelry1);
        items.Add(Jewelry2);
        items.Add(Jewelry3);
    }

    void CreateRunes()
    {
        Item Rune1 = new Item(4001,
            new List<Stat>
            {
                //Rune planlamasindan sonra her runun verecegi belli statlar duruma gore buralara eklenebilir.
            },
            new List<Stat>(), (int)eItemType.Rune, 0, 1, 300, 60, 0);

        Item Rune2 = new Item(4002,
            new List<Stat>
            {
                //Rune planlamasindan sonra her runun verecegi belli statlar duruma gore buralara eklenebilir.
            },
            new List<Stat>(), (int)eItemType.Rune, 0, 1, 500, 100, 0);

        Item Rune3 = new Item(4003,
            new List<Stat>
            {
                //Rune planlamasindan sonra her runun verecegi belli statlar duruma gore buralara eklenebilir.
            },
            new List<Stat>(), (int)eItemType.Rune, 0, 1, 700, 150, 0);

        items.Add(Rune1);
        items.Add(Rune2);
        items.Add(Rune3);
    }

    void CreateResources()
    {
        Item Wood = new Item(5001,
            new List<Stat>(),
            new List<Stat>(), (int)eItemType.Resource, 0, 99, 5, 1, 0);

        Item Stone = new Item(5002,
            new List<Stat>(),
            new List<Stat>(), (int)eItemType.Resource, 0, 99, 6, 1, 0);

        items.Add(Wood);
        items.Add(Stone);
    }

    void CreatePotions()
    {
        //daha sonra.
    }
}
