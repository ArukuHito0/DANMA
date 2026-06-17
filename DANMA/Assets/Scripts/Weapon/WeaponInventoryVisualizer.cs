using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryVisualizer : MonoBehaviour
{
    [SerializeField] private List<WeaponIcon> weaponIcons;

    private void Start()
    {
        PlayerController.weaponInventory.OnWeaponSlotChanged += VisualizeInventory;
    }

    private void OnDestory()
    {
        PlayerController.weaponInventory.OnWeaponSlotChanged -= VisualizeInventory;
    }

    private void VisualizeInventory(WeaponData data, int idx)
    {
        weaponIcons[idx].Initialize(data, idx);
    }
}
