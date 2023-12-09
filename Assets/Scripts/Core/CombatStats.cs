using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats : MonoBehaviour
{
    public List<float> Stats = new List<float>();
    public float CurrentHp;
    public float CurrentMana;
    public float CurrentStamina;
    public bool isDead;

    public CCState ccstate;
    public void Start()
    {
        GameManager.instance.SetMyStats(this);

        CalculateStats();
        CurrentHp = Stats[(int)eStat.MaxHealth];
        CurrentMana = Stats[(int)eStat.MaxMana];
        CurrentStamina = Stats[(int)eStat.Stamina];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CalculateStats();
        }
    }

    public void CalculateStats()
    {
        Stats.Clear();
        int length = (int)eStat.Length;
        for (int i = 0; i < length; i++)
            Stats.Add(0); //gelebilecek tum statlar icin yuva aciyoruz.

        SetBaseStats(); //0 olarak baslayamayacak statlara base deger atiyoruz, Max hp, damage vb.
        SetEquipmentsStats(); // su anda kusanilmis ekipmanlarin main ve ekstra bonuslarindan gelen degerleri hesapla.
        SetPercentStatEffects(); //yuzde bazli bonus veren statlarin normal oranlarin uzerine eklenmesi.
        RefreshCharacterStat(); //Update UI to visualize stats.
    }

    public void RefreshCharacterStat()
    {
        UIHandler.instance.UpdateStatPanel(Stats);
    }

    void SetBaseStats()
    {
        Stats[(int)eStat.MaxHealth] = 100;
        Stats[(int)eStat.MaxMana] = 25;
        Stats[(int)eStat.Damage] = 30;
        Stats[(int)eStat.PhysicalDefence] = 12;
        Stats[(int)eStat.MagicalDefence] = 9;
        Stats[(int)eStat.CritDamage] = 30;
        Stats[(int)eStat.CritRate] = 5;
        Stats[(int)eStat.HealthRegeneration] = Stats[(int)eStat.MaxHealth] * 0.05f;
        Stats[(int)eStat.ManaRegeneration] = Stats[(int)eStat.MaxMana] * 0.05f;
        Stats[(int)eStat.HealAmount] = 20;
    }

    void SetEquipmentsStats()
    {
        int length = GameManager.instance.CurrentActiveSave.Equipments.Count;
        for (int i = 0; i < length; i++)
        {
            if (GameManager.instance.CurrentActiveSave.Equipments[i].id != 0)
            {
                Item currentEquipment = GameManager.instance.CurrentActiveSave.Equipments[i];
                int length2 = currentEquipment.BaseStats.Count;
                for (int x = 0; x < length2; x++)
                {
                    Stat currentStat = currentEquipment.BaseStats[x];
                    Stats[currentStat.id] += currentStat.amount;
                }

                int length3 = currentEquipment.BonusStats.Count;
                for (int y = 0; y < length3; y++)
                {
                    Stat currentStat = currentEquipment.BaseStats[y];
                    Stats[currentStat.id] += currentStat.amount;
                }
            }
        }
    }

    void SetPercentStatEffects()
    {
        Stats[(int)eStat.MaxHealth] = Stats[(int)eStat.MaxHealth] + Stats[(int)eStat.MaxHealth] * Stats[(int)eStat.MaxHealthPercent] / 100;
        Stats[(int)eStat.MaxMana] = Stats[(int)eStat.MaxMana] + Stats[(int)eStat.MaxMana] * Stats[(int)eStat.MaxManaPercent] / 100;
        Stats[(int)eStat.Damage] = Stats[(int)eStat.Damage] + Stats[(int)eStat.Damage] * Stats[(int)eStat.DamgePercent] / 100;
        Stats[(int)eStat.Stamina] = Stats[(int)eStat.Stamina] + Stats[(int)eStat.Stamina] * Stats[(int)eStat.StaminaPercent] / 100;
        Stats[(int)eStat.PhysicalDefence] = Stats[(int)eStat.PhysicalDefence] + Stats[(int)eStat.PhysicalDefence] * Stats[(int)eStat.PhysicalDefencePercent] / 100;
        Stats[(int)eStat.MagicalDefence] = Stats[(int)eStat.MagicalDefence] + Stats[(int)eStat.MagicalDefence] * Stats[(int)eStat.MagicalDefencePercent] / 100;
        Stats[(int)eStat.HealthRegeneration] = Stats[(int)eStat.HealthRegeneration] + Stats[(int)eStat.HealthRegeneration] * Stats[(int)eStat.HealthRegenerationPercent] / 100;
        Stats[(int)eStat.ManaRegeneration] = Stats[(int)eStat.ManaRegeneration] + Stats[(int)eStat.ManaRegeneration] * Stats[(int)eStat.ManaRegenerationPercent] / 100;
        Stats[(int)eStat.HealAmount] = Stats[(int)eStat.HealAmount] + Stats[(int)eStat.HealAmount] * Stats[(int)eStat.HealAmountPercent] / 100;
    }

    public void GetDamage(CombatStats _enemyStats, float hitPercent, float skillPercent)
    {
        if (isDead)
            return;

        float damage = _enemyStats.Stats[(int)eStat.Damage];
        if (hitPercent > 0)
            damage -= Stats[(int)eStat.PhysicalDefence];

        if (skillPercent > 0)
            damage -= Stats[(int)eStat.MagicalDefence];

        if (damage <= 5)
            damage = Random.Range(10,21);

        damage = damage + damage * (hitPercent / 100f);
        damage = damage + damage * (skillPercent / 100f);

        if (Random.Range(0, 101) < Stats[(int)eStat.Evasion] - _enemyStats.Stats[(int)eStat.Accuracy])
        {
            CreateDamageVisual("Dodge", false,false,false,false);
            return;
        }

        bool isCriticalHit = false;
        if (Random.Range(0,101) < _enemyStats.Stats[(int)eStat.CritRate])
        {
            isCriticalHit = true;
            damage = damage + damage * (_enemyStats.Stats[(int)eStat.CritDamage] / 100f);
        }

        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            Dead();

            if (Random.Range(0, 101) < 30)
                _enemyStats.HealHP(_enemyStats.Stats[(int)eStat.HealAmount] / 100f * _enemyStats.Stats[(int)eStat.MaxHealth], false);

            if (Random.Range(0, 101) < 30)
                _enemyStats.HealMana(_enemyStats.Stats[(int)eStat.HealAmount] / 100f * _enemyStats.Stats[(int)eStat.MaxMana], false);
        }

        bool hasBurn = false;
        bool hasFreeze = false;
        bool hasPoison = false;
        if (Random.Range(0,101) < (_enemyStats.Stats[(int)eStat.BurnChance] - Stats[(int)eStat.BurnRessist]))
        {
            ApplyCC(CCState.Burning);
            hasBurn = true;
        }
        
        if(!hasBurn)
            if (Random.Range(0, 101) < (_enemyStats.Stats[(int)eStat.FreezeChance] - Stats[(int)eStat.FreezeRessist]))
            {
                ApplyCC(CCState.Freezing);
                hasFreeze = true;
            }

        if (!hasBurn && !hasFreeze)
            if (Random.Range(0, 101) < (_enemyStats.Stats[(int)eStat.PoisonChance] - Stats[(int)eStat.PoisonRessist]))
            {
                ApplyCC(CCState.Poisoning);
                hasPoison = true;
            }

        //if (_enemyStats.Stats[(int)eStat.LifeSteal] > 0)
            //_enemyStats.HealHP(_enemyStats.Stats[(int)eStat.LifeSteal] / 100f * damage, true);

        //if (_enemyStats.Stats[(int)eStat.ManaSteal] > 0)
            //_enemyStats.HealMana(_enemyStats.Stats[(int)eStat.LifeSteal] / 100f * damage, true);

        CreateDamageVisual(damage.ToString(), isCriticalHit, hasBurn, hasFreeze, hasPoison);
    }

    public void HealHP(float _amount, bool _isSteal)
    {
        Debug.Log(name + " got " + _amount + " hp healing.");
        CurrentHp += _amount;
        if (CurrentHp > Stats[(int)eStat.MaxHealth])
            CurrentHp = Stats[(int)eStat.MaxHealth];

        //kac can doldugunu gosteren text.
    }

    public void HealMana(float _amount, bool _isSteal)
    {
        Debug.Log(name + " got " + _amount + " mana healing.");
        CurrentMana += _amount;
        if (CurrentMana > Stats[(int)eStat.MaxMana])
            CurrentMana = Stats[(int)eStat.MaxMana];

        //kac mana doldugunu gosteren text.
    }

    public void ApplyCC(CCState _state)
    {
        ccstate = _state;
    }

    public void Dead()
    {
        isDead = true;
    }

    public void CreateDamageVisual(string damage, bool isCritical, bool _hasBurn, bool _hasFreeze, bool _hasPoison)
    {

    }

    public enum CCState
    {
        none,
        Burning,
        Freezing,
        Poisoning,
    }
}
