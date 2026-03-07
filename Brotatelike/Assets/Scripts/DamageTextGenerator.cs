using ObjectPoolSystem;
using TMPro;
using UnityEngine;

public class DamageTextGenerator : MonoBehaviour
{
    private ObjectPool pool;

    private void OnEnable()
    {
        HealthComponent.OnDamaged += DisplayDamage;
    }

    private void OnDisable()
    {
        HealthComponent.OnDamaged -= DisplayDamage;
    }

    private void Awake()
    {
        pool = GameObject.Find("DamageTextPool").GetComponent<ObjectPool>();
    }

    private void DisplayDamage(Vector3 pos, int damage)
    {
        DamageText text = pool.GetPooledObject(pos).GetComponent<DamageText>();
        text.SetDamageText(damage);
    }
}
