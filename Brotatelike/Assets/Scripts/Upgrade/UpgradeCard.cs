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
    private PlayerController player;
    private UIScaler scaler;

    private RectTransform upgradeCard;

    [SerializeField] private UpgradeBaseData upgrade;

    private Image upgradeFrame;
    private Image upgradeIcon;
    private TextMeshProUGUI upgradeName;
    private TextMeshProUGUI upgradeEffect;
    private TextMeshProUGUI upgradeEffectValue;

    public static event Action<string, bool> OnCloseUpgrade;

    private void OnEnable()
    {
        upgradeFrame.color = upgrade.GetUpgradeColor();
        upgradeIcon.sprite = upgrade.upgradeIcon;
        upgradeName.text = upgrade.upgradeName;
        upgradeEffect.text = upgrade.GetEffectName();
        upgradeEffectValue.text = upgrade.GetEffectValue();
    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        scaler = GetComponent<UIScaler>();
        upgradeCard = GetComponent<RectTransform>();
        upgradeFrame = transform.Find("Frame").GetComponent<Image>();
        upgradeIcon = transform.Find("UpgradeIcon").GetComponent<Image>();
        upgradeName = transform.Find("UpgradeTitleText").GetComponent<TextMeshProUGUI>();
        upgradeEffect = transform.Find("UpgradeEffectNameText").GetComponent<TextMeshProUGUI>();
        upgradeEffectValue = transform.Find("UpgradeEffectValueText").GetComponent<TextMeshProUGUI>();
    }

    public void ChooseUpgrade()
    {
        OnCloseUpgrade?.Invoke("UpgradeUI", false);
        OnCloseUpgrade?.Invoke("StatusUI", false);

        upgrade.Upgrade(player);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Normal);
    }
}
