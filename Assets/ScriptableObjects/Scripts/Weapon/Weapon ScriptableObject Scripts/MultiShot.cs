using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/MultiShot Weapon")]
public class MultiShot : Weapon
{
    private StaticWeaponStats stats;
    private PlayerHandler _handler;
    private float coolDownTimer;
    private bool readyToFire;
    private Transform target;
    private Transform firePoint;


    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        Debug.Log("Initialize Weapon");
        stats = _s;
        _handler = _w.GetComponent<PlayerHandler>();
        readyToFire = true;
        firePoint = _w.GetFirePoint();
        GameManager.i.SetupObjectPools(stats.spawnedObject.GetComponent<MultiShotProjectileHandler>(), 50, "Multi", PoolType.Projectiles);
    }

    public override void TryActivateWeapon()
    {
        if(readyToFire)
        {
            for(int i = 0; i < 8; i++)
            {
            MultiShotProjectileHandler newProjectile = ObjectPooler.DequeueObject<MultiShotProjectileHandler>("Multi", PoolType.Projectiles);
            newProjectile.transform.position = firePoint.position;
            newProjectile.transform.eulerAngles = new Vector3(0, 0, i * 45f);
            newProjectile.gameObject.SetActive(true);
            newProjectile.Initialize(stats.damage, stats.lifeTime, 
                _handler.GetStats().GetProjectileSpeed(), _handler.GetStats().GetCritChance());
            }
            ActivateWeaponCooldown();
        }
    }

    public override void UpdateWeaponTimers()
    {
        if(!readyToFire) coolDownTimer -= Time.deltaTime;
        if(coolDownTimer <= 0) readyToFire = true;
    }
    public override void ActivateWeaponCooldown()
    {
        readyToFire = false;
        coolDownTimer = stats.coolDown;
    }
    public override StaticWeaponStats GetWeaponStats() {return stats;}
}
