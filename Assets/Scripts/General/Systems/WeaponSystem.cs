using System.Collections.Generic;
using UnityEngine;


public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private Dictionary<Weapon, int> equippedWeapons;
    public void InitializeFreshGame(WeaponSO startingWeapon)
    {
        Debug.Log("Initialize Weapon System");
        equippedWeapons = new Dictionary<Weapon, int>();
        EquipNewWeapon(startingWeapon);
    }

    public void InitializeContinuedGame()
    {
        
    }
    void Update()
    {
        ActivateWeapons();
        UpdateWeaponTimers();
    }
    private void ActivateWeapons()
    {
        foreach(Weapon weapon in equippedWeapons.Keys)
            weapon.TryActivateWeapon();
    }

    private void UpdateWeaponTimers()
    {
        foreach(Weapon weapon in equippedWeapons.Keys)
            weapon.UpdateWeaponTimers();
    }
    private void EquipNewWeapon(WeaponSO _w)
    {
        equippedWeapons.Add(_w.weapon, _w.ID);
        _w.weapon.InitializeWeapon(this);
    }
    private void UnEquipWeapon(Weapon _w)
    {
        
    }
    private void SaveWeapons()
    {
        
    }
    public Transform GetFirePoint(){return firePoint;}
}
