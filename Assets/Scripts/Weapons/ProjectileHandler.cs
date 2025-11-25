using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private int damage;
    private float lifeTime, lifeTimer, speed, critChance;

    public void Initialize(Transform _t, int _d, float _lt, float _s, float _c)
    {
        Debug.Log("Initialize Bullet");
        target = _t;
        damage = _d;
        lifeTime = _lt;
        rb = GetComponent<Rigidbody2D>();
        speed = _s;
        critChance = _c;
    }

    private void Update() 
    {
        if(target == null || target.gameObject.activeInHierarchy == false) { ObjectPooler.EnqueueObject(this, "Bullet"); return;}

        Vector3 moveDir = (target.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
        
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        Debug.Log($"Bullet Hit Something {trigger.gameObject}");
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = Random.Range(0f,100f) < critChance;

        if(damageable != null) 
            damageable.TakeDamage(isCritical);

        ObjectPooler.EnqueueObject(this, "Bullet");
    }
}
