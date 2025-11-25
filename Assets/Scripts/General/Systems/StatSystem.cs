using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{ health, speed, damage, fireRate, critChance, critBonus, penetration, range, projectileSpeed }
public class StatSystem
{
    #region Base Variables
    private int baseHealth;
    private float baseMoveSpeed, baseAttackPower, baseFireRate, baseCritChance, baseCritDamageBonus,
        basePenetration, baseAttackRange, baseProjectileSpeed;
    //Enemy Specific
    private Brain[] brains;
    #endregion
    #region Stat Bonuses
    private int bonusHealth;
    private float bonusMoveSpeed, bonusAttackPower, bonusFireRate, bonusCritChance, bonusCritDamageBonus,
        bonusPenetration, bonusAttackRange, bonusProjectileSpeed;
    #endregion
    #region Actual Stats
    private int actualHealth;
    private float actualMoveSpeed, actualAttackPower, actualFireRate, actualCritChance, actualCritDamageBonus,
        actualPenetration, actualAttackRange, actualProjectileSpeed;
    #endregion 
    #region Initialize Stats
    public StatSystem (PlayerStatsSO _stats)
    {
        actualHealth = _stats.baseHealth;
        actualMoveSpeed = _stats.baseMoveSpeed;
        actualAttackPower = _stats.baseAttackPower;
        actualFireRate = _stats.baseFireRate;
        actualCritChance = _stats.baseCritChance;
        actualCritDamageBonus = _stats.baseCritDamageBonus;
        actualPenetration = _stats.basePenetration;
        actualAttackRange = _stats.baseAttackRange;
        actualProjectileSpeed = _stats.baseProjectileSpeed;
    }
    public StatSystem (EnemyStatsSO _stats)
    {
        baseHealth = _stats.health;
        baseMoveSpeed = _stats.moveSpeed;
    }
    #endregion
    #region Update Stat Bonuses
    public void UpdateStats(int _value)
    {
        bonusHealth += _value;
        actualHealth = baseHealth + bonusHealth;
    }
    public void UpdateStats(float _value, StatType _type)
    {
        switch(_type)
        {
            case StatType.speed:
                bonusMoveSpeed += _value;
                actualMoveSpeed = baseMoveSpeed + bonusMoveSpeed;
                break;
            case StatType.damage:
                bonusAttackPower += _value;
                actualAttackPower = baseAttackPower + bonusAttackPower;
                break;
            case StatType.fireRate:
                bonusFireRate += _value;
                actualFireRate = baseFireRate - bonusFireRate;
                break;
            case StatType.critChance:
                bonusCritChance += _value;
                actualCritChance = baseCritChance + bonusCritChance;
                break;
            case StatType.critBonus:
                bonusCritDamageBonus += _value;
                actualCritDamageBonus = baseCritDamageBonus + bonusCritDamageBonus;
                break;
            case StatType.penetration:
                bonusPenetration += _value;
                actualPenetration = basePenetration + bonusPenetration;
                break;
            case StatType.range:
                bonusAttackRange += _value;
                actualAttackRange = baseAttackRange + bonusAttackRange;
                break;
            case StatType.projectileSpeed:
                bonusProjectileSpeed += _value;
                actualProjectileSpeed = baseProjectileSpeed + bonusProjectileSpeed;
                break;
        }
    }
    #endregion
    #region Get Stats
    public int GetHealth (){return actualHealth;}
    public float GetMoveSpeed(){return actualMoveSpeed;}
    public float GetAttackPower(){return actualAttackPower;}
    public float GetFireRate(){return actualFireRate;}
    public float GetCritChance(){return actualCritChance;}
    public float GetActualCritDamageBonus(){return actualCritDamageBonus;}
    public float GetPenetration(){return actualPenetration;}
    public float GetAttackRange(){return actualAttackRange;}
    public float GetProjectileSpeed(){return actualProjectileSpeed;}
    public Brain[] GetBrains(){return brains;}
    #endregion
}
