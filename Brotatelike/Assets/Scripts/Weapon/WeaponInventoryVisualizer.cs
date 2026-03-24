using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryVisualizer : MonoBehaviour
{
    [SerializeField] List<WeaponIcon> weaponIcons;

    private void Start()
    {
        PlayerController.weaponInventory.OnWeaponSlotChanged += VisualizeInventory;
    }

    private void OnDisable()
    {
        PlayerController.weaponInventory.OnWeaponSlotChanged -= VisualizeInventory;
    }

    private void VisualizeInventory(WeaponData data, int idx)
    {
        weaponIcons[idx].Initialize(data);
    }
}
