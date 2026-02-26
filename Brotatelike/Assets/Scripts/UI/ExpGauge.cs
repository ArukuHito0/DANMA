using TMPro;
using UnityEngine;

public class ExpGauge : GaugeUIBar
{
    [SerializeField] private ExpComponent expComponent;
    [SerializeField] private TextMeshProUGUI expProgressText;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    private void OnDisable()
    {
        expComponent.OnExpChanged -= UpdateFillAmount;
        expComponent.OnLevelChanged -= UpdateLevelText;
    }

    private void Awake()
    {
        expComponent.OnExpChanged += UpdateFillAmount;
        expComponent.OnLevelChanged += UpdateLevelText;

        UpdateLevelText();
    }

    public override void UpdateFillAmount(float rate)
    {
        base.UpdateFillAmount(rate);

        expProgressText.text = $"{expComponent.exp} / {expComponent.levelUpExp}";
    }

    private void UpdateLevelText()
    {
        currentLevelText.text = $"Lv.<size=50>{expComponent.CurrentLevel.ToString("D2")}";
    }
}
