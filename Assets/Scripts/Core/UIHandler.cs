using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    public Material SelectedHeaderMat;
    public Material UnselectedHeaderMat;

    public int CurrentActivePanel;

    public Transform OffensiveStatsParent, DefensiveStatParent;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnSelectedHeader(int _index)
    {
        Transform buttonsParent = transform.GetChild(0).GetChild(0);
        Transform panelParent = transform.GetChild(0).GetChild(1);
        Transform target = panelParent.GetChild(_index);
        foreach (Transform item in buttonsParent)
        {
            item.GetComponent<Button>().interactable = true;
            item.GetComponent<Image>().material = UnselectedHeaderMat;
        }

        foreach (Transform item in panelParent)
        {
            if(item != target)
            CloseMenu(item,Vector3.right);
        }
        buttonsParent.GetChild(_index).GetComponent<Button>().interactable = false;
        buttonsParent.GetChild(_index).GetComponent<Image>().material = SelectedHeaderMat;
        CurrentActivePanel = _index;
        OpenMenu(target, Vector3.right);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
            {
                CloseMenu(transform.GetChild(0), Vector3.one);
            }
            else
            {
                InventoryHandler.instance.RefreshInventoryUI();
                OpenMenu(transform.GetChild(0), Vector3.one);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            if (transform.GetChild(0).gameObject.activeInHierarchy)
                CloseMenu(transform.GetChild(0), Vector3.one);
    }

    public void UpdateStatPanel(List<float> _stats)
    {
        //Offensive Stats Damage, Crit Damage, Crit Rate, Accuracy, Attack Speed, Shot Distance, Burn Chance, Freeze Chance, Poison Chance,
        OffensiveStatsParent.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.Damage) +": ";
        OffensiveStatsParent.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.Damage) + _stats[(int)eStat.Damage];

        OffensiveStatsParent.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.CritDamage) + ": ";
        OffensiveStatsParent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.CritDamage) + _stats[(int)eStat.CritDamage];

        OffensiveStatsParent.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.CritRate) + ": ";
        OffensiveStatsParent.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.CritRate) + _stats[(int)eStat.CritRate];

        OffensiveStatsParent.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.Accuracy) + ": ";
        OffensiveStatsParent.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.Accuracy) + _stats[(int)eStat.Accuracy];

        OffensiveStatsParent.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.AttackSpeed) + ": ";
        OffensiveStatsParent.GetChild(9).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.AttackSpeed) + _stats[(int)eStat.AttackSpeed];

        OffensiveStatsParent.GetChild(10).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.ShotDistance) + ": ";
        OffensiveStatsParent.GetChild(11).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.ShotDistance) + _stats[(int)eStat.ShotDistance];

        OffensiveStatsParent.GetChild(12).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.BurnChance) + ": ";
        OffensiveStatsParent.GetChild(13).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.BurnChance) + _stats[(int)eStat.BurnChance];

        OffensiveStatsParent.GetChild(14).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.FreezeChance) + ": ";
        OffensiveStatsParent.GetChild(15).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.FreezeChance) + _stats[(int)eStat.FreezeChance];

        OffensiveStatsParent.GetChild(16).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.PoisonChance) + ": ";
        OffensiveStatsParent.GetChild(17).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.PoisonChance) + _stats[(int)eStat.PoisonChance];


        //Defensive Stats Max Health, Max Mana, Phy Defence, Mgc Defence, Heal Amount, Stamina, Evasion, healthRegeneration, Mana Regeneration, BurnResist, FreezeResist, PoisonResist
        DefensiveStatParent.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.MaxHealth) + ": ";
        DefensiveStatParent.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.MaxHealth) + _stats[(int)eStat.MaxHealth];

        DefensiveStatParent.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.MaxMana) + ": ";
        DefensiveStatParent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.MaxMana) + _stats[(int)eStat.MaxMana];

        DefensiveStatParent.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.PhysicalDefence) + ": ";
        DefensiveStatParent.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.PhysicalDefence) + _stats[(int)eStat.PhysicalDefence];

        DefensiveStatParent.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.MagicalDefence) + ": ";
        DefensiveStatParent.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.MagicalDefence) + _stats[(int)eStat.MagicalDefence];

        DefensiveStatParent.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.HealAmount) + ": ";
        DefensiveStatParent.GetChild(9).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.HealAmount) + _stats[(int)eStat.HealAmount];

        DefensiveStatParent.GetChild(10).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.Stamina) + ": ";
        DefensiveStatParent.GetChild(11).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.Stamina) + _stats[(int)eStat.Stamina];

        DefensiveStatParent.GetChild(12).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.BurnRessist) + ": ";
        DefensiveStatParent.GetChild(13).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.BurnRessist) + _stats[(int)eStat.BurnRessist];

        DefensiveStatParent.GetChild(14).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.FreezeRessist) + ": ";
        DefensiveStatParent.GetChild(15).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.FreezeRessist) + _stats[(int)eStat.FreezeRessist];

        DefensiveStatParent.GetChild(16).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.PoisonRessist) + ": ";
        DefensiveStatParent.GetChild(17).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.PoisonRessist) + _stats[(int)eStat.PoisonRessist];

        DefensiveStatParent.GetChild(18).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.HealthRegeneration) + ": ";
        DefensiveStatParent.GetChild(19).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.HealthRegeneration) + _stats[(int)eStat.HealthRegeneration];

        DefensiveStatParent.GetChild(20).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.ManaRegeneration) + ": ";
        DefensiveStatParent.GetChild(21).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.ManaRegeneration) + _stats[(int)eStat.ManaRegeneration];

        DefensiveStatParent.GetChild(22).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetTextOf("Stat" + (int)eStat.Evasion) + ": ";
        DefensiveStatParent.GetChild(23).GetComponent<TMPro.TextMeshProUGUI>().text = LanguageDatabase.instance.GetSymbolOfStat((int)eStat.Evasion) + _stats[(int)eStat.Evasion];
    }

    Vector3 GetMirrorVector(Vector3 originalVector)
    {
        // Mirror the vector by replacing 0 with 1 and 1 with 0
        return new Vector3(
            originalVector.x == 0 ? 1 : 0,
            originalVector.y == 0 ? 1 : 0,
            originalVector.z == 0 ? 1 : 0
        );
    }

    public void OpenMenu(Transform _menu, Vector3 axis)
    {
        _menu.localScale = GetMirrorVector(axis);
        StopCoroutine(OpenMenuNumerator(_menu, axis));
        StopCoroutine(CloseMenuNumerator(_menu, axis));
        StartCoroutine(OpenMenuNumerator(_menu, axis));
    }

    IEnumerator OpenMenuNumerator(Transform _menu, Vector3 axis)
    {
        _menu.gameObject.SetActive(true);
        while ((axis.y == 1 && _menu.localScale.y < 1) || (axis.x == 1 && _menu.localScale.x < 1))
        {
            _menu.localScale += axis * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        _menu.localScale = Vector3.one;
    }

    public void CloseMenu(Transform _menu, Vector3 axis)
    {
        _menu.localScale = Vector3.one;
        StopCoroutine(OpenMenuNumerator(_menu, axis));
        StopCoroutine(CloseMenuNumerator(_menu, axis));
        StartCoroutine(CloseMenuNumerator(_menu, axis));
    }

    public Transform GetPanelParent(Panels _target)
    {
        return transform.GetChild(0).GetChild(1).GetChild((int)_target);
    }

    IEnumerator CloseMenuNumerator(Transform _menu, Vector3 axis)
    {
        while ((axis.y == 1 && _menu.localScale.y > 0.25f) || (axis.x == 1 && _menu.localScale.x > 0.25f))
        {
            _menu.localScale -= axis * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        _menu.localScale = Vector3.zero;
        _menu.gameObject.SetActive(false);
    }

    public enum Panels
    {
        StatPanel,
        WeaponPanel,
        ArmourPanel,
        JevelriesPanel,
        RunesPanel,
        SkillsPanel,
        QuestsPanel,
        MapPanel,
        SettingsPanel
    }
}
