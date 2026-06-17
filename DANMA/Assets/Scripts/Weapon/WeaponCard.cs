using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    private WeaponData weaponData;

    [SerializeField] private Image iconBackground;
    [SerializeField] private Image weaponFrame;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponDescription;
    
    public ClickableButton chooseButton;

    private void Start()
    {
        chooseButton._OnClick.AddListener(() => EnemyGenerator.Instance.StartWave());
    }

    public void Initialize(WeaponData data)
    {
        weaponData = data;

        Color color = weaponData.Tier.GetTierColor();
        iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
        weaponFrame.color = color;
        weaponIcon.sprite = weaponData.Icon;
        weaponName.text = weaponData.Name;
        weaponDescription.text = weaponData.GetDescriptionText();
    }

    public void WeaponGet()
    {
        PlayerController.weaponInventory.AddWeapon(weaponData);
    }
}
