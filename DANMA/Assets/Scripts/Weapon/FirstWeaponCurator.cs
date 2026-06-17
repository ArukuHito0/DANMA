using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Å‰‚ÉŠl“¾‚·‚é•Ší‚ğ’ñ¦‚·‚éƒNƒ‰ƒX
/// </summary>
public class FirstWeaponCurator : MonoBehaviour
{
    [SerializeField] private WeaponLottery weaponLottery;

    [SerializeField] private Canvas firstWeaponUI;
    [SerializeField] private List<WeaponCard> weaponCards;

    private void Awake()
    {
        foreach (WeaponCard weaponCard in weaponCards)
        {
            weaponCard.chooseButton._OnClick.AddListener(() => firstWeaponUI.enabled = false);
        }
    }

    public void ChooseFirstWeapons()
    {
        firstWeaponUI.enabled = true;

        List<WeaponData> weapons = weaponLottery.GetRandomWeaponDatasByTier(TierType.Common, weaponCards.Count);

        for (int i = 0; i < weaponCards.Count; i++)
        {
            weaponCards[i].Initialize(weapons[i]);
        }
    }
}
