using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLvText;
    
    [SerializeField] private TextMeshProUGUI mainStatusNameText;
    [SerializeField] private TextMeshProUGUI mainStatusValueText;

    [SerializeField] private TextMeshProUGUI subStatusNameText;
    [SerializeField] private TextMeshProUGUI subStatusValueText;

    private void OnDestroy()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= MainStatusDisplay;
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= SubStatusDisplay;
    }

    private void Start()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += MainStatusDisplay;
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += SubStatusDisplay;

        MainStatusDisplay();
        SubStatusDisplay();
    }

    private void MainStatusDisplay()
    {
        currentLvText.text = $"Lv.<size=50>{PlayerController.Instance.ExpComponent.CurrentLevel}";

        mainStatusNameText.text =
                              PlayerStatus.MaxHealth.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Strength.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Critical.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackRange.GetPlayerStatusName() + "\n" +
                              PlayerStatus.MoveSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Armor.GetPlayerStatusName() + "\n" +
                              PlayerStatus.DodgeChance.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Luck.GetPlayerStatusName();

        mainStatusValueText.text =
                               PlayerStatus.MaxHealth.GetRuntimeStatus() + $" | {StatToColorText(PlayerStatus.MaxHealth)}" + "\n" +
                               StatToColorText(PlayerStatus.Strength) + "\n" +
                               StatToColorText(PlayerStatus.AttackSpeed) + "\n" +
                               StatToColorText(PlayerStatus.Critical) + "\n" +
                               StatToColorText(PlayerStatus.AttackRange) + "\n" +
                               StatToColorText(PlayerStatus.MoveSpeed) + "\n" +
                               StatToColorText(PlayerStatus.Armor) + "\n" +
                               StatToColorText(PlayerStatus.DodgeChance) + "\n" +
                               StatToColorText(PlayerStatus.Luck);
    }

    private void SubStatusDisplay()
    {

        subStatusNameText.text =
                              PlayerStatus.CollectRange.GetPlayerStatusName() + "\n" +
                              PlayerStatus.GetGoldRate.GetPlayerStatusName() + "\n" +
                              PlayerStatus.GetExpRate.GetPlayerStatusName() + "\n" +
                              PlayerStatus.FreeRerollCnt.GetPlayerStatusName() + "\n" +
                              PlayerStatus.WaveHeal.GetPlayerStatusName() + "\n" +
                              PlayerStatus.WaveGetGold.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackHeal.GetPlayerStatusName() + "\n" +
                              PlayerStatus.EnemySpawnRate.GetPlayerStatusName() + "\n" +
                              PlayerStatus.SaleSpawnChance.GetPlayerStatusName() + "\n" +
                              PlayerStatus.ItemPriceRate.GetPlayerStatusName();

        subStatusValueText.text = 
                               StatToColorText(PlayerStatus.CollectRange) + "\n" +
                               StatToColorText(PlayerStatus.GetGoldRate) + "\n" +
                               StatToColorText(PlayerStatus.GetExpRate) + "\n" +
                               StatToColorText(PlayerStatus.FreeRerollCnt) + "\n" +
                               StatToColorText(PlayerStatus.WaveHeal) + "\n" +
                               StatToColorText(PlayerStatus.WaveGetGold) + "\n" +
                               StatToColorText(PlayerStatus.AttackHeal) + "\n" +
                               StatToColorText(PlayerStatus.EnemySpawnRate, true) + "\n" +
                               StatToColorText(PlayerStatus.SaleSpawnChance) + "\n" +
                               StatToColorText(PlayerStatus.ItemPriceRate);
    }

    public string StatToColorText(PlayerStatus status, bool reverse = false)
    {
        float baseStatus = status.GetBaseStatus();
        float bonusStatus = status.GetBonusStatus();

        if(bonusStatus == 0)
            return $"{bonusStatus.ToString("F0")}";
        else if (bonusStatus > 0)
            if(reverse)
                return $"<color=red>{bonusStatus.ToString("F0")}</color>";
            else
                return $"<color=green>{bonusStatus.ToString("F0")}</color>";
        else
            if (reverse)
                return $"<color=green>{bonusStatus.ToString("F0")}</color>";
            else
                return $"<color=red>{bonusStatus.ToString("F0")}</color>";
    }
}
