using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : ClickableButton
{
    public WeaponData weaponData {  get; private set; }

    [SerializeField] private Image weaponIcon;
    [SerializeField] private Image iconBackground;
    [SerializeField] private Image weaponFrame;

    [SerializeField] private ProductPanel productPanel;

    private int slotIdx = -1;

    public void Initialize(WeaponData data, int idx)
    {
        slotIdx = idx;

        if (data == null)
        {
            weaponIcon.enabled = false;
            weaponData = null;
            iconBackground.color = new Color(1, 1, 1, 0.078f);
            weaponFrame.color = Color.white;
            Debug.Log("表示で参照するデータがありません");
        }
        else
        {
            weaponIcon.enabled = true;
            weaponData = data;
            weaponIcon.sprite = data.Icon;
            Color color = weaponData.Tier.GetTierColor();
            iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
            weaponFrame.color = color;
        }
    }

    public void OpenWeaponPanel()
    {
        if (weaponData == null) return;

        productPanel.gameObject.SetActive(true);

        productPanel.UpdatePanelVisual(weaponData);
    }

    public void CloseWeaponPanel()
    {
        if (weaponData == null) return;

        if (productPanel.isLock) return;

        productPanel.gameObject.SetActive(false);
    }

    public void LockWeaponPanel()
    {
        if(weaponData == null) return;

        productPanel.LockPanel(slotIdx);
    }
}
