using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public abstract void InitializeWeapon(WeaponSystem _w);
    public abstract void TryActivateWeapon();
    public abstract void ActivateWeaponCooldown();
    public abstract void UpdateWeaponTimers();
    public abstract StaticWeaponStats GetWeaponStats();
}
