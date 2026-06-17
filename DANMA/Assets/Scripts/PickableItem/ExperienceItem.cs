using UnityEngine;

public class ExperienceItem : PickableItem
{
    [SerializeField] private int experiencePoint;

    protected override void OnPickUpItem()
    {
        SoundUtil.PlaySe("GetExp");

        var result = experiencePoint * (1 + PlayerStatus.GetExpRate.GetRuntimeStatus() * 0.01f);

        PlayerController.Instance.ExpComponent.AddExp((int)result);
    }
}
