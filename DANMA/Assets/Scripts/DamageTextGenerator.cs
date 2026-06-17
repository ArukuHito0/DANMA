using UnityEngine;

public class DamageTextGenerator : MonoBehaviour
{
    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private DamageText criticalTextPrefab;
    [SerializeField] private DamageText healTextPrefab;
    [SerializeField] private DamageText dodgeTextPrefab;

    private void OnEnable()
    {
        HealthComponent.OnDamaged += DisplayDamage;
        HealthComponent.OnCriticalDamaged += DisplayCritical;
        HealthComponent.OnHealed += DisplayHeal;
        HealthComponent.OnDodgeSuccess += DisplayDodge;
    }

    private void OnDisable()
    {
        HealthComponent.OnDamaged -= DisplayDamage;
        HealthComponent.OnCriticalDamaged -= DisplayCritical;
        HealthComponent.OnHealed -= DisplayHeal;
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

    private void DisplayHeal(Vector3 pos, int heal)
    {
        ObjectPoolManager.Instance.GetPooledObject(healTextPrefab, pos).SetDamageText(heal);
    }

    private void DisplayDodge(Vector3 pos)
    {
        ObjectPoolManager.Instance.GetPooledObject(dodgeTextPrefab, pos).StartAnim();
    }
}
