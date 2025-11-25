using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Stats / Static Weapon Stats")]
public class StaticWeaponStats: ScriptableObject
{
    public int damage;
    public float coolDown;
    public float lifeTime;
}
