using UnityEngine;

public class DamageTextGenerator : MonoBehaviour
{
    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private DamageText criticalTextPrefab;
    [SerializeField] private DamageText dodgeTextPrefab;

    private void OnEnable()
    {
        HealthComponent.OnDamaged += DisplayDamage;
        HealthComponent.OnCriticalDamaged += DisplayCritical;
        HealthComponent.OnDodgeSuccess += DisplayDodge;
    }

    private void OnDisable()
    {
        HealthComponent.OnDamaged -= DisplayDamage;
        HealthComponent.OnCriticalDamaged -= DisplayCritical;
        HealthComponent.OnDodgeSuccess -= DisplayDodge;
    }

    private void DisplayDamage(Vector3 pos, int damage)
    {
        ObjectPoolManager.Instance.GetPooledObject(damageTextPrefab, pos).SetDamageText(damage);
    }

    private void DisplayCritical(Vector3 pos, int damage)
    {
        ObjectPoolManager.Instance.GetPooledObject(criticalTextPrefab, pos).SetDamageText(damage);
    }

    private void DisplayDodge(Vector3 pos)
    {
        ObjectPoolManager.Instance.GetPooledObject(dodgeTextPrefab, pos).StartAnim();
    }
}
