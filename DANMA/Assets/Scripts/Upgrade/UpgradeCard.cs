using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private UpgradeData upgrade;

    [SerializeField] private Image upgradeFrame;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeEffect;
    [SerializeField] private TextMeshProUGUI upgradeEffectValue;

    public static event Action<string, bool> OnCloseUpgrade;

    private void OnEnable()
    {
        upgradeFrame.color = upgrade.tier.GetTierColor();
        upgradeIcon.sprite = upgrade.upgradeIcon;
        upgradeName.text = upgrade.upgradeName;
        upgradeEffect.text = upgrade.GetUpgradeName();
        upgradeEffectValue.text = upgrade.GetUpgradeValueText();
    }

    public void ChooseUpgrade()
    {
        OnCloseUpgrade?.Invoke("UpgradeUI", false);
        OnCloseUpgrade?.Invoke("StatusUI", false);

        upgrade.Upgrade();

        TimeManager.SetTimeMode(TimeManager.TimeMode.Normal);
    }
}
