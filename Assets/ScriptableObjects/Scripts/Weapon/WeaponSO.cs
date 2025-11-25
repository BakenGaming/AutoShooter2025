using UnityEngine;

[CreateAssetMenu(menuName ="WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public int ID;
    public Weapon weapon;
    public Sprite uiSprite;
}
