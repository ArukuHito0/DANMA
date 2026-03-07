using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusNameText;
    [SerializeField] private TextMeshProUGUI statusValueText;

    private void OnEnable()
    {
        if (PlayerController.Instance != null)
            PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += MainStatusDisplay;
    }

    private void OnDisable()
    {
        if(PlayerController.Instance != null)
            PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= MainStatusDisplay;
    }

    private void Start()
    {
        MainStatusDisplay();
    }

    private void MainStatusDisplay()
    {
        statusNameText.text = PlayerStatus.MaxHealth.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Strength.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Critical.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackRange.GetPlayerStatusName() + "\n" +
                              PlayerStatus.MoveSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Armor.GetPlayerStatusName() + "\n" +
                              PlayerStatus.DodgeChance.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Luck.GetPlayerStatusName();

        statusValueText.text = PlayerController.Instance.playerRuntimeStatus.MaxHealth.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.Strength.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.AttackSpeed.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.Critical.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.BonusAttackRange.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.BonusMoveSpeed.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.Armor.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.DodgeChance.ToString("F0") + "\n" +
                               PlayerController.Instance.playerRuntimeStatus.Luck.ToString("F0");
    }
}
