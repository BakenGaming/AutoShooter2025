using UnityEngine;
using System;

public class OrbitHandler : MonoBehaviour
{
    public static event Action OnOrbiterDeactivate;
    private Transform pivot;
    private int damage;
    private float lifeTimer, speed, critChance;
    public void InitializeOrbiter(Transform _p, int _d, float _lt, float _s, float _c)
    {
        Debug.Log("Initialize Orbiter");
        pivot = _p;
        damage = _d;
        lifeTimer = _lt;
        speed = _s;
        critChance = _c;
    }
    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, speed * Time.deltaTime);
        //UpdateTimers();
    }

    private void UpdateTimers()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0) DeactivateOrbiter();
    }
    private void DeactivateOrbiter()
    {
        OnOrbiterDeactivate?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        Debug.Log($"Orbiter Hit Something {trigger.gameObject}");
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = UnityEngine.Random.Range(0f,100f) < critChance;

        if(damageable != null) 
            damageable.TakeDamage(damage, isCritical);
    }
}
