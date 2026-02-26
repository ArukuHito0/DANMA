using System;
using TMPro;
using UnityEngine;

public class HealthGauge : GaugeUIBar
{
    [SerializeField]
    private HealthComponent healthComponent;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void OnDisable()
    {
        if(healthComponent != null)
            healthComponent.OnHealthChanged += UpdateFillAmount;
        if(PlayerRuntimeStatus.Instance != null)
            PlayerRuntimeStatus.Instance.OnMaxHealthUpdate += UpdateFillAmount;
    }

    private void Awake()
    {
        if (healthComponent != null)
            healthComponent.OnHealthChanged += UpdateFillAmount;
        if (PlayerRuntimeStatus.Instance != null)
            PlayerRuntimeStatus.Instance.OnMaxHealthUpdate += UpdateFillAmount;
    }

    public override void UpdateFillAmount(float rate)
    {
        base.UpdateFillAmount(rate);

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"{healthComponent.CurrentHealth} <size=25>/ {PlayerRuntimeStatus.Instance?.MaxHealth}";
    }
}
