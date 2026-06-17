using UnityEngine;

public class HealItem : PickableItem
{
    [SerializeField] private int healValue;

    protected override void OnPickUpItem()
    {
        SoundUtil.PlaySe("GetExp");

        PlayerController.Instance.HealthComponent.Heal(healValue);
    }
}
