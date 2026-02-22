using UnityEngine;

public interface IUpgrade
{
    void Upgrade(PlayerController player);

    string GetEffectName();

    string GetEffectValue();
}
