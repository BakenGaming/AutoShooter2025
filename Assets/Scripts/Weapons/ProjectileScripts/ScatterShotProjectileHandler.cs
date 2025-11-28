using UnityEngine;

public class ScatterShotProjectileHandler : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private int damage;
    private float lifeTimer, speed, critChance, disabledTimer;
    private bool isScatter, isDisabled;

    public void Initialize(Transform _t, int _d, float _lt, float _s, float _c)
    {
        Debug.Log($"Initialize Bullet {_t}");
        isScatter = false;
        target = _t;
        damage = _d;
        lifeTimer = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    public void InitializeScatter(int _d, float _lt, float _s, float _c)
    {
        isScatter = true;
        damage = _d;
        lifeTimer = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
        GetComponent<CircleCollider2D>().enabled = false;
        isDisabled = true;
        disabledTimer = .1f;
    }

    private void Update() 
    {
        if(isScatter)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            if(target == null || target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, "Scatter", PoolType.Projectiles); return;}
            Vector3 moveDir = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle - 90f);
            transform.position += moveDir * speed * Time.deltaTime;
        }
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateProjectile();
        if(isDisabled) disabledTimer -= Time.deltaTime;
        if(isDisabled && disabledTimer <= 0) EnableCollider();
    }
    private void EnableCollider(){GetComponent<CircleCollider2D>().enabled = true;}

    private void DeactivateProjectile()
    {
        ObjectPooler.EnqueueObject(this, "Scatter", PoolType.Projectiles);
    }
    private void Scatter(Transform _pos)
    {
        Debug.Log("Scatter");
        for(int i = 0; i < 4; i++)
        {
            ScatterShotProjectileHandler newProjectile = ObjectPooler.DequeueObject<ScatterShotProjectileHandler>("Scatter", PoolType.Projectiles);
            newProjectile.transform.position = _pos.position;
            newProjectile.transform.eulerAngles = new Vector3(0, 0, i * 90f);
            newProjectile.gameObject.SetActive(true);
            newProjectile.InitializeScatter(damage / 2, lifeTimer, speed, critChance);
        }
        //ObjectPooler.EnqueueObject(this, "Scatter", PoolType.Projectiles);
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < critChance;

        if(damageable != null)
        {
            damageable.TakeDamage(damage, isCritical);
            if(!isScatter) Scatter(trigger.transform);
            else ObjectPooler.EnqueueObject(this, "Scatter", PoolType.Projectiles);
        }
    }
}
