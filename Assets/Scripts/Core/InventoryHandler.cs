using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    public static InventoryHandler instance { get; private set; }

    public Transform WeaponsParent, ArmoursParent, JewelriesParent;
    public Transform WeaponsTooltip, ArmoursTooltip, JewelriesTooltip;
    private Item lastSelectedWeapon, LastSelectedArmour, LastSelectedJewelry;
    public Transform WeaponTooltipBonus, ArmourTooltipBonus, JevelryTooltipBonus;

    public Button[] TooltipButtons;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        TooltipButtons[0].onClick.AddListener(()=>EquipButton(0));
        TooltipButtons[0].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(()=>UnequipButton(0));
        TooltipButtons[1].onClick.AddListener(()=>EquipButton(1));
        TooltipButtons[1].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => UnequipButton(1));
        TooltipButtons[2].onClick.AddListener(()=>EquipButton(2));
        TooltipButtons[2].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => UnequipButton(2));
    }

    private void Start()
    {
        lastSelectedWeapon = ItemDatabase.instance.GetItem(0);
        LastSelectedArmour = ItemDatabase.instance.GetItem(0);
        LastSelectedJewelry = ItemDatabase.instance.GetItem(0);
    }

    //Item eklerken kac adet item eklenicekse itemin currentAmount miktarina degeri girip gonderin. CurrentAmountun Max Amountu gecmediginden emin olun!
    public void AddItem(Item _item)
    {
        List<Item> inventory = GameManager.instance.CurrentActiveSave.Inventory;

        //Once item ekleyebilecegimiz uygun slotlari bir arayalim, eger item ust uste eklenebiliyorsa.
        if (_item.maxAmount > 1)
        {
            List<int> possibleSlots = new List<int>();
            int length = inventory.Count;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    if (_item.id == inventory[i].id)
                    {
                        possibleSlots.Add(i);
                    }
                }
            }

            if (possibleSlots.Count > 0) //Ust uste ekleyebilecegimiz alternatifler, yeni slot acmamiza gerek kalmayabilir.
            {
                //Miktarin maks miktari gecmediginden ve tum miktarlari da alabildigimizden emin olmamiz lazim.
                do
                {
                    if(possibleSlots.Count > 0) //Hala uygun yer var mi? eger kalmadiysa yeni slot acip whiledan kurtulalim.
                    {
                        int possibleAmount = inventory[possibleSlots[0]].maxAmount - inventory[possibleSlots[0]].currentAmount;
                        if (possibleAmount > _item.currentAmount)
                        {
                            //Yeterince alan var bu slotta, tum itemi buraya ekleyip whiledan kurtulabiliriz.
                            inventory[possibleSlots[0]].currentAmount += _item.currentAmount;
                            break;
                        }
                        else
                        {
                            //Bu slotta bir miktar alan var ama yeterli degil (Mesela max limit 99, bu slotta 98 var ama biz 4 adet item ekliyoruz gibi...) Bu durumda alabildigimizi alip, while in sonraki turuna gecicez.
                            inventory[possibleSlots[0]].currentAmount = inventory[possibleSlots[0]].maxAmount; //her sekilde bu slot fullenmis olacak, direkt max a esitleyebiliriz.
                            _item.currentAmount -= possibleAmount; //mumkun olan miktari aldik ve itemdeki ekleyecegimiz miktardan dustuk.
                        }
                    }
                    else
                    {
                        inventory.Add(new Item(_item));
                        break;
                    }
                    
                } while (_item.currentAmount > 0); //amount 0 a dusene kadar bu itemi uygun slotlara dagitacagiz.
            }
            else //hic uygun slot yok, bu durumda tek sansimiz yeni slot acmak.
            {
                inventory.Add(new Item(_item));
            }
        }
        else
        {
            //Item ust uste eklenmiyor, yeni slot acmaktan baska caremiz yok.
            inventory.Add(new Item(_item)); //her ihtimale karsi itemi clonladigimizdan emin olalim yoksa ummadik buglarla karsilasabiliriz orjinal itemlerin datalarini degistirirsek.
        }

        RefreshInventoryUI();
        GameManager.instance.MyStats.CalculateStats();
    }

    public void RemoveItem(int _index, int _amount, bool _removeCompletely = false)
    {
        
    }

    public void RefreshInventoryUI()
    {
        int length = WeaponsParent.childCount;
        for (int i = length - 1; i >= 0; i--)
            Destroy(WeaponsParent.GetChild(i).gameObject);

        length = ArmoursParent.childCount;
        for (int i = length - 1; i >= 0; i--)
            Destroy(ArmoursParent.GetChild(i).gameObject);

        length = JewelriesParent.childCount;
        for (int i = length - 1; i >= 0; i--)
            Destroy(JewelriesParent.GetChild(i).gameObject);

        Item firstWeaponItem = ItemDatabase.instance.GetItem(0);
        Item firstArmourItem = ItemDatabase.instance.GetItem(0);
        Item firstJewelryItem = ItemDatabase.instance.GetItem(0);
        //once ekipmanlar 1.siraya koyalim hep ustte olsunlar kusanilmis itemler
        List<Item> Equipments = GameManager.instance.CurrentActiveSave.Equipments;
        foreach (var item in Equipments)
        {
            GameObject newSlot = null;
            if (item.type == (int)eItemType.MainWeapon)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), WeaponsParent);
                firstWeaponItem = item;
                TooltipButtons[0].transform.GetChild(0).gameObject.SetActive(true);
                TooltipButtons[0].transform.GetChild(1).gameObject.SetActive(false);
                TooltipButtons[0].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Unequip_Button");
            }
            else if (item.type == (int)eItemType.Chest || item.type == (int)eItemType.Helmet || item.type == (int)eItemType.Pant || item.type == (int)eItemType.wristlet || item.type == (int)eItemType.Boot)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), ArmoursParent);
                firstArmourItem = item;
                TooltipButtons[1].transform.GetChild(0).gameObject.SetActive(true);
                TooltipButtons[1].transform.GetChild(1).gameObject.SetActive(false);
                TooltipButtons[1].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Unequip_Button");
            }
            else if (item.type == (int)eItemType.Necklace || item.type == (int)eItemType.Earring || item.type == (int)eItemType.Ring || item.type == (int)eItemType.Belt || item.type == (int)eItemType.Bracelet)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), JewelriesParent);
                firstJewelryItem = item;
                TooltipButtons[2].transform.GetChild(0).gameObject.SetActive(true);
                TooltipButtons[2].transform.GetChild(1).gameObject.SetActive(false);
                TooltipButtons[2].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Unequip_Button");
            }
            if(newSlot != null)
            {
                newSlot.GetComponent<InventorySlot>().SetSlot(Equipments.IndexOf(item), SlotType.Equipment);
                SetItemUI(item, newSlot.transform,true);
            }
        }

        //Simdi tum itemleri uygun slotlara dagitalim
        List<Item> inventory = GameManager.instance.CurrentActiveSave.Inventory;
        foreach (var item in inventory)
        {
            GameObject newSlot = null;
            if (item.type == (int)eItemType.MainWeapon)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), WeaponsParent);
                if(firstWeaponItem.id == 0)
                {
                    firstWeaponItem = item;
                    TooltipButtons[0].transform.GetChild(0).gameObject.SetActive(false);
                    TooltipButtons[0].transform.GetChild(1).gameObject.SetActive(true);
                    TooltipButtons[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Equip_Button");
                }
            }
            else if (item.type == (int)eItemType.Chest || item.type == (int)eItemType.Helmet || item.type == (int)eItemType.Pant || item.type == (int)eItemType.wristlet || item.type == (int)eItemType.Boot)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), ArmoursParent);
                if (firstArmourItem.id == 0)
                {
                    firstArmourItem = item;
                    TooltipButtons[1].transform.GetChild(0).gameObject.SetActive(false);
                    TooltipButtons[1].transform.GetChild(1).gameObject.SetActive(true);
                    TooltipButtons[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Equip_Button");
                }
            }
            else if (item.type == (int)eItemType.Necklace || item.type == (int)eItemType.Earring || item.type == (int)eItemType.Ring || item.type == (int)eItemType.Belt || item.type == (int)eItemType.Bracelet)
            {
                newSlot = Instantiate(Resources.Load<GameObject>("UI/ItemElement"), JewelriesParent);
                if (firstJewelryItem.id == 0)
                {
                    firstJewelryItem = item;
                    TooltipButtons[2].transform.GetChild(0).gameObject.SetActive(false);
                    TooltipButtons[2].transform.GetChild(1).gameObject.SetActive(true);
                    TooltipButtons[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Equip_Button");
                }
            }
            if (newSlot != null)
            {
                newSlot.GetComponent<InventorySlot>().SetSlot(inventory.IndexOf(item), SlotType.Inventory);
                SetItemUI(item, newSlot.transform,false);
            }
        }

        if (firstWeaponItem.id != 0)
        {
            lastSelectedWeapon = firstWeaponItem;
            ShowTooltip(firstWeaponItem);
        }
        if (firstArmourItem.id != 0)
        {
            LastSelectedArmour = firstArmourItem;
            ShowTooltip(firstArmourItem);
        }
        if (firstJewelryItem.id != 0)
        {
            LastSelectedJewelry = firstJewelryItem;
            ShowTooltip(firstJewelryItem);
        }
    }

    void SetItemUI(Item theItem, Transform currentSlot, bool _isEquipped)
    {
        TextMeshProUGUI itemname = currentSlot.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemdesc = currentSlot.GetChild(1).GetComponent<TextMeshProUGUI>();
        Image itemicon = currentSlot.GetChild(2).GetChild(0).GetComponent<Image>();
        TextMeshProUGUI equippedText = currentSlot.GetChild(3).GetComponent<TextMeshProUGUI>();
        if (_isEquipped)
        {
            equippedText.gameObject.SetActive(true);
            equippedText.text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Equipped");
        }
        else
            equippedText.gameObject.SetActive(false);

        itemname.text = LanguageDatabase.instance.GetTextOf("Item_" + theItem.id);
        itemdesc.text = LanguageDatabase.instance.GetTextOf("Item_" + theItem.id + "_Desc");
        itemicon.sprite = Resources.Load<Sprite>("Icon/" + theItem.id);
    }

    public void EquipButton(int panel)
    {
        Item equippingItem = null;
        if (panel == 0)
            equippingItem = lastSelectedWeapon;
        else if (panel == 1)
            equippingItem = LastSelectedArmour;
        else if (panel == 2)
            equippingItem = LastSelectedJewelry;

        if(equippingItem != null)
        {
            EquipItem(equippingItem);
        }
        else
        {
            Debug.LogError("Can not equip. Selected Item not found!");
        }
    }

    public void UnequipButton(int panel)
    {
        Item unequippingItem = null;
        if (panel == 0)
            unequippingItem = lastSelectedWeapon;
        else if (panel == 1)
            unequippingItem = LastSelectedArmour;
        else if (panel == 2)
            unequippingItem = LastSelectedJewelry;

        if (unequippingItem != null)
        {
            UnequipItem(unequippingItem);
        }
        else
        {
            Debug.LogError("Can not equip. Selected Item not found!");
        }
    }

    public void EquipItem(Item _equippingItem)
    {
        int inventorySlot = -1;
        int length = GameManager.instance.CurrentActiveSave.Inventory.Count;
        for (int i = 0; i < length; i++)
        {
            if (_equippingItem == GameManager.instance.CurrentActiveSave.Inventory[i])
            {
                Debug.Log("Equipping item has found at slot " + i);
                inventorySlot = i;
                break;
            }
        }

        if (inventorySlot == -1)
        {
            Debug.LogError("Equipping item could not be found in inventory! Equipping is not possible!");
            return;
        }

        Item currentEquippingItem = GameManager.instance.CurrentActiveSave.Equipments[_equippingItem.type];
        if (currentEquippingItem.id == 0) //slot bos, direkt olarak itemi ekipmanlar icindeki slotuna tasiyabiliriz.
        {
            GameManager.instance.CurrentActiveSave.Equipments[_equippingItem.type] = new Item(_equippingItem);
            GameManager.instance.CurrentActiveSave.Inventory.RemoveAt(inventorySlot);
        }
        else
        {
            //Slotta baska bir silah var, o zaman bunlari replace edicez.
            Item unequippingItem = new Item(GameManager.instance.CurrentActiveSave.Equipments[_equippingItem.type]);
            GameManager.instance.CurrentActiveSave.Equipments[_equippingItem.type] = new Item(_equippingItem);
            GameManager.instance.CurrentActiveSave.Inventory[inventorySlot] = unequippingItem;
        }

        RefreshInventoryUI();
        GameManager.instance.MyStats.CalculateStats();
    }

    public void UnequipItem(Item _unequippingItem)
    {
        int equipmentSlot = -1;
        int length = GameManager.instance.CurrentActiveSave.Equipments.Count;
        for (int i = 0; i < length; i++)
        {
            if (_unequippingItem == GameManager.instance.CurrentActiveSave.Equipments[i])
            {
                Debug.Log("Unequipping item has found at slot " + i);
                equipmentSlot = i;
                break;
            }
        }

        if (equipmentSlot == -1)
        {
            Debug.LogError("Unequipping item could not be found in inventory! Equipping is not possible!");
            return;
        }

        GameManager.instance.CurrentActiveSave.Inventory.Add(new Item(_unequippingItem));
        GameManager.instance.CurrentActiveSave.Equipments[equipmentSlot] = ItemDatabase.instance.GetItem(0);

        RefreshInventoryUI();
        GameManager.instance.MyStats.CalculateStats();
    }

    public void ShowTooltip(Item theItem)
    {
        Transform currentTooltip = null;
        Transform currentTooltipBonus = null;
        if (theItem.type == (int)eItemType.MainWeapon)
        {
            currentTooltip = WeaponsTooltip;
            currentTooltipBonus = WeaponTooltipBonus;
            lastSelectedWeapon = theItem;
        }
        else if (theItem.type == (int)eItemType.Chest || theItem.type == (int)eItemType.Helmet || theItem.type == (int)eItemType.Pant || theItem.type == (int)eItemType.wristlet || theItem.type == (int)eItemType.Boot)
        {
            currentTooltip = ArmoursTooltip;
            currentTooltipBonus = ArmourTooltipBonus;
            LastSelectedArmour = theItem;
        }
        else if (theItem.type == (int)eItemType.Necklace || theItem.type == (int)eItemType.Earring || theItem.type == (int)eItemType.Ring || theItem.type == (int)eItemType.Belt || theItem.type == (int)eItemType.Bracelet)
        {
            currentTooltip = JewelriesTooltip;
            currentTooltipBonus = JevelryTooltipBonus;
            LastSelectedJewelry = theItem;
        }

        if (theItem.id == 0)
            currentTooltip.gameObject.SetActive(false);
        else
        {
            currentTooltip.gameObject.SetActive(true);
            TextMeshProUGUI itemname = currentTooltip.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            Image itemicon = currentTooltip.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>();
            TextMeshProUGUI itemdesc = currentTooltip.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemsellprice = currentTooltip.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>();

            itemname.text = LanguageDatabase.instance.GetTextOf("Item_" + theItem.id);
            itemdesc.text = LanguageDatabase.instance.GetTextOf("Item_" + theItem.id + "_Desc");
            itemicon.sprite = Resources.Load<Sprite>("Icon/" + theItem.id);
            itemsellprice.text = theItem.sellToShopPrice + " " + LanguageDatabase.instance.GetTextOf("Gold");

            if (currentTooltipBonus != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    currentTooltipBonus.GetChild(0 + (i * 2)).GetComponent<TextMeshProUGUI>().text = "";
                    currentTooltipBonus.GetChild(1 + (i * 2)).GetComponent<TextMeshProUGUI>().text = "";
                }
                int statAmount = theItem.BaseStats.Count;
                int statBonusAmount = theItem.BonusStats.Count;
                int a = 0;
                for (int i = 0; i < statAmount; i++)
                {
                    if (i >= 4)
                    {
                        a = 4;
                        break;
                    }
                    currentTooltipBonus.GetChild(0 + (a * 2)).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + theItem.BaseStats[i].id) + ": ";
                    currentTooltipBonus.GetChild(1 + (a * 2)).GetComponent<TextMeshProUGUI>().text = "+" + LanguageDatabase.instance.GetSymbolOfStat(theItem.BaseStats[i].id) + " " + theItem.BaseStats[i].amount;
                    a++;
                }
                for (int i = 0; i < statBonusAmount; i++)
                {
                    if (a >= 4)
                        break;

                    currentTooltipBonus.GetChild(0 + (a * 2)).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + theItem.BonusStats[i].id) + ": ";
                    currentTooltipBonus.GetChild(1 + (a * 2)).GetComponent<TextMeshProUGUI>().text = "+" + LanguageDatabase.instance.GetSymbolOfStat(theItem.BaseStats[i].id) + " " + theItem.BonusStats[i].amount;
                    a++;
                }
            }
        }
    }

    public void OnSlotSelected(int _index, SlotType _type)
    {
        int panelIndex = -1;
        if (UIHandler.instance.CurrentActivePanel == (int)UIHandler.Panels.WeaponPanel)
            panelIndex = 0;
        else if (UIHandler.instance.CurrentActivePanel == (int)UIHandler.Panels.ArmourPanel)
            panelIndex = 1;
        else if (UIHandler.instance.CurrentActivePanel == (int)UIHandler.Panels.JevelriesPanel)
            panelIndex = 2;

        if (panelIndex == -1)
            return;

        if (_type == SlotType.Equipment)
        {
            ShowTooltip(GameManager.instance.CurrentActiveSave.Equipments[_index]);
            TooltipButtons[panelIndex].transform.GetChild(0).gameObject.SetActive(true);
            TooltipButtons[panelIndex].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Unequip_Button");
            TooltipButtons[panelIndex].transform.GetChild(1).gameObject.SetActive(false);
            SelectAnItem(GameManager.instance.CurrentActiveSave.Equipments[_index]);
        }
        else
        {
            ShowTooltip(GameManager.instance.CurrentActiveSave.Inventory[_index]);
            TooltipButtons[panelIndex].transform.GetChild(0).gameObject.SetActive(false);
            TooltipButtons[panelIndex].transform.GetChild(1).gameObject.SetActive(true);
            TooltipButtons[panelIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Text_Tooltip_Equip_Button");
            SelectAnItem(GameManager.instance.CurrentActiveSave.Inventory[_index]);
        }
    }

    public void SelectAnItem(Item _selectedItem)
    {
        if (_selectedItem.type == (int)eItemType.MainWeapon)
        {
            lastSelectedWeapon = _selectedItem;
        }
        else if (_selectedItem.type == (int)eItemType.Chest || _selectedItem.type == (int)eItemType.Helmet || _selectedItem.type == (int)eItemType.Pant || _selectedItem.type == (int)eItemType.wristlet || _selectedItem.type == (int)eItemType.Boot)
        {
            LastSelectedArmour = _selectedItem;
        }
        else if (_selectedItem.type == (int)eItemType.Necklace || _selectedItem.type == (int)eItemType.Earring || _selectedItem.type == (int)eItemType.Ring || _selectedItem.type == (int)eItemType.Belt || _selectedItem.type == (int)eItemType.Bracelet)
        {
            LastSelectedJewelry = _selectedItem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            List<int> testItemList = new List<int>() { 1001,2001,3001,4001,5001,6001,7001,8001,9001,10001, 11001};
            AddItem(ItemDatabase.instance.GetItem(testItemList[Random.Range(0, testItemList.Count)])); //Random.Range(1, 6)
        }
    }

    public enum SlotType
    {
        Inventory,
        Equipment,
    }
}
