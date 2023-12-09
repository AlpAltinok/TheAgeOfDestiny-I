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
    }

    void CreateWeapons()
    {
        Item Weapon1 = new Item(1001,
            new List<Stat>
            {
                new Stat((int)eStat.Damage, 25),
            },
            new List<Stat>(), (int)eItemType.MainWeapon, 0, 1, 40, 10, 10);

        Item Weapon2 = new Item(1002,
            new List<Stat>
            {
                new Stat((int)eStat.Damage, 35),
            },
            new List<Stat>(), (int)eItemType.MainWeapon, 0, 1, 100, 30, 15);

        Item Weapon3 = new Item(1003,
            new List<Stat>
            {
                new Stat((int)eStat.Damage, 45),
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
                new Stat((int)eStat.PhysicalDefence, 30),
                new Stat((int)eStat.MagicalDefence, 25)
            },
            new List<Stat>(), (int)eItemType.Chest, 0, 1, 20, 5, 15);

        Item Pant1 = new Item(3001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 25),
                new Stat((int)eStat.MagicalDefence, 22)
            },
            new List<Stat>(), (int)eItemType.Pant, 0, 1, 60, 15, 13);

        Item Helmet1 = new Item(4001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 17),
                new Stat((int)eStat.MagicalDefence, 14)
            },
            new List<Stat>(), (int)eItemType.Helmet, 0, 1, 110, 25, 11);

        Item Wristlet1 = new Item(5001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 12),
                new Stat((int)eStat.MagicalDefence, 7)
            },
            new List<Stat>(), (int)eItemType.wristlet, 0, 1, 110, 25, 8);

        Item Boot1 = new Item(6001,
            new List<Stat>
            {
                new Stat((int)eStat.PhysicalDefence, 10),
                new Stat((int)eStat.MagicalDefence, 5)
            },
            new List<Stat>(), (int)eItemType.Boot, 0, 1, 110, 25, 8);

        items.Add(Armour1);
        items.Add(Pant1);
        items.Add(Helmet1);
        items.Add(Wristlet1);
        items.Add(Boot1);
    }

    void CreateJewelries()
    {
        Item Necklace1 = new Item(7001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 60),
                new Stat((int)eStat.MaxMana, 50)
            },
            new List<Stat>(), (int)eItemType.Necklace, 0, 1, 340, 50, 15);

        Item Earring1 = new Item(8001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 40),
                new Stat((int)eStat.MaxMana, 20)
            },
            new List<Stat>(), (int)eItemType.Earring, 0, 1, 120, 100, 13);

        Item Ring1 = new Item(9001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 40),
                new Stat((int)eStat.MaxMana, 20)
            },
            new List<Stat>(), (int)eItemType.Ring, 0, 1, 120, 150, 13);

        Item Bracelet1 = new Item(10001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 40),
                new Stat((int)eStat.MaxMana, 20)
            },
            new List<Stat>(), (int)eItemType.Bracelet, 0, 1, 120, 150, 13);

        Item Belt1 = new Item(11001,
            new List<Stat>
            {
                new Stat((int)eStat.MaxHealth, 40),
                new Stat((int)eStat.MaxMana, 20)
            },
            new List<Stat>(), (int)eItemType.Belt, 0, 1, 120, 150, 13);

        items.Add(Necklace1);
        items.Add(Earring1);
        items.Add(Ring1);
        items.Add(Bracelet1);
        items.Add(Belt1);
    }
}
