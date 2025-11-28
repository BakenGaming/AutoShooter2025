using UnityEngine;

public class ExplosiveProjectileHandler : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private int damage;
    private float lifeTimer, speed, critChance, blastRadius;

    public void Initialize(Transform _t, int _d, float _lt, float _s, float _c, float _b)
    {
        target = _t;
        damage = _d;
        lifeTimer = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
        blastRadius = _b;
    }

    private void Update() 
    {
        if(target == null || target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, "Explosive", PoolType.Projectiles); return;}

        Vector3 moveDir = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        
        transform.position += moveDir * speed * Time.deltaTime;
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateProjectile();
    }
    private void DeactivateProjectile()
    {
        ObjectPooler.EnqueueObject(this, "Explosive", PoolType.Projectiles);
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < critChance;

        if(damageable != null)
        {
            EnemyHandler _h;
            Collider2D[] damageables = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            for(int i = 0; i < damageables.Length; i++)
            {
                _h = damageables[i].gameObject.GetComponent<EnemyHandler>();
                if(_h != null)
                {
                    _h.TakeDamage(damage, isCritical);
                }
            }
        }
        ObjectPooler.EnqueueObject(this, "Explosive", PoolType.Projectiles);
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(target.transform.position, blastRadius);
    // }
}
