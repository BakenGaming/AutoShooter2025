using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Scatter Weapon")]
public class ScatterShot : Weapon
{
    private StaticWeaponStats stats;
    private PlayerHandler _handler;
    private float coolDownTimer;
    private bool readyToFire;
    private Transform target;
    private Transform firePoint;
    public override void InitializeWeapon(WeaponSystem _w, StaticWeaponStats _s)
    {
        _handler = _w.GetComponent<PlayerHandler>();
        stats = _s;
        readyToFire = true;
        firePoint = _w.GetFirePoint();
        GameManager.i.SetupObjectPools(stats.spawnedObject.GetComponent<ScatterShotProjectileHandler>(), 50, "Scatter", PoolType.Projectiles);
    }
    public override void TryActivateWeapon()
    {
        if(readyToFire)
        {
            target = FindTarget();
            if(target != null)
            {
                //Debug.Log("Shoot");
                ScatterShotProjectileHandler newProjectile = ObjectPooler.DequeueObject<ScatterShotProjectileHandler>("Scatter", PoolType.Projectiles);
                newProjectile.transform.position = firePoint.position;
                newProjectile.transform.rotation = firePoint.rotation;
                newProjectile.gameObject.SetActive(true);
                newProjectile.Initialize(target, stats.damage, stats.lifeTime, 
                    _handler.GetStats().GetProjectileSpeed(), _handler.GetStats().GetCritChance());
            }
            ActivateWeaponCooldown();
        }
    }
    public override void ActivateWeaponCooldown()
    {
        readyToFire = false;
        coolDownTimer = stats.coolDown;
    }

    public override void UpdateWeaponTimers()
    {
        if(!readyToFire) coolDownTimer -= Time.deltaTime;
        if(coolDownTimer <= 0) readyToFire = true;
    }

    private Transform FindTarget()
    {
        float distancetoClosestEnemy = Mathf.Infinity;
        EnemyHandler closestEnemy = null;
        EnemyHandler[] allEnemies = FindObjectsByType<EnemyHandler>(FindObjectsSortMode.None);
        
        foreach(EnemyHandler currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - GameManager.i.GetPlayerGO().transform.position).sqrMagnitude;
            if(distanceToEnemy < distancetoClosestEnemy)
            {
                distancetoClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        //Debug.Log(closestEnemy);
        return closestEnemy.transform;        
    }
    public override StaticWeaponStats GetWeaponStats(){return stats;}
}
