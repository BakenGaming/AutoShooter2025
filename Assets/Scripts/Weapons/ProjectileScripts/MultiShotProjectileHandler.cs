using UnityEngine;

public class MultiShotProjectileHandler : MonoBehaviour
{
private Rigidbody2D rb;
    private int damage;
    private float lifeTimer, speed, critChance;

    public void Initialize(int _d, float _lt, float _s, float _c)
    {
        damage = _d;
        lifeTimer = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
    }

    private void Update() 
    {
        transform.position += transform.right * speed * Time.deltaTime;
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateProjectile();
    }
    private void DeactivateProjectile()
    {
        ObjectPooler.EnqueueObject(this, "Multi", PoolType.Projectiles);
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        Debug.Log($"Bullet Hit Something {trigger.gameObject}");
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < critChance;

        if(damageable != null) 
            damageable.TakeDamage(damage, isCritical);

        ObjectPooler.EnqueueObject(this, "Multi", PoolType.Projectiles);
    }
}
