using TMPro;
using UnityEngine;

public class ExpGauge : GaugeUIBar
{
    [SerializeField]
    private ExpComponent expComponent;
    [SerializeField]
    private TextMeshProUGUI currentLevelText;

    private void OnDestroy()
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

    private void UpdateLevelText()
    {
        currentLevelText.text = $"Lv.<size=50>{expComponent.CurrentLevel}";
    }
}
