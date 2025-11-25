using UnityEngine;

[CreateAssetMenu(menuName ="Base Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    public int baseHealth;
    public float baseMoveSpeed;
    public float baseAttackPower;
    public float baseFireRate;
    public float baseCritChance;
    public float baseCritDamageBonus;
    public float basePenetration;
    public float baseAttackRange;
    public float baseProjectileSpeed;
}
