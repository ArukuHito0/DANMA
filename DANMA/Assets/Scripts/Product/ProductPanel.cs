using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    IProduct product;

    [SerializeField] private Image iconBackground;
    [SerializeField] private Image productFrame;
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productEffect;

    [SerializeField] private Image selectedBackground;

    private int selectedSlotIdx = -1;

    public bool isLock { get; private set; } = false;

    public void UpdatePanelVisual(IProduct product)
    {
        if (this.product != product || this.product == null)
        {
            this.product = product;
        }

        Color color = this.product.Tier.GetTierColor();
        iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
        productFrame.color = color;
        productIcon.sprite = this.product.Icon;
        productName.text = this.product.Name;
        productEffect.text = this.product.GetDescriptionText();
    }

    public void UpgradeWeapon()
    {
        PlayerController.weaponInventory.UpgradeWeapon((WeaponData)product);
    }

    public void SellWeapon()
    {
        if (PlayerController.weaponInventory.GetWeaponCnt() <= 1) return;

        PlayerController.weaponInventory.RemoveWeapon(selectedSlotIdx);
        PlayerController.Instance.wallet.AddMoney((int)(product.Price * 0.5f));

        product = null;
    }

    public void ClosePanel()
    {
        UnlockPanel();

        gameObject.SetActive(false);
    }

    public void LockPanel(int idx)
    {
        selectedSlotIdx = idx;

        if (product != null)
        {
            isLock = true;

            if (selectedBackground != null)
                selectedBackground.enabled = true;
        }
    }

    public void UnlockPanel()
    {
        isLock = false;

        if (selectedBackground != null)
            selectedBackground.enabled = false;
    }
}
