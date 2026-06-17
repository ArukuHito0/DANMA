using System.Diagnostics;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ResultValueContainer resultContainer;

    [SerializeField] private FirstWeaponCurator weaponCurator;

    [SerializeField] private TextMeshProUGUI resultTitleText;
    [SerializeField] private TextMeshProUGUI surviveWaveCntText;
    [SerializeField] private TextMeshProUGUI totalDefeatedEnemiesCntText;
    [SerializeField] private TextMeshProUGUI totalDamageText;
    [SerializeField] private TextMeshProUGUI totalGetGoldText;
    [SerializeField] private TextMeshProUGUI totalSpendGoldText;
    [SerializeField] private TextMeshProUGUI totalGetExpText;

    private void Awake()
    {
        resultContainer.ResetData();

        DamageCalculator.OnCalculateDamage += resultContainer.AddTotalDamage;
        Wallet.OnMoneyAdded += resultContainer.AddTotalGetGold;
        Wallet.OnMoneyRemoved += resultContainer.AddTotalSpendGold;
        ExpComponent.OnExpAdded += resultContainer.AddTotalExp;
    }

    private void OnDestroy()
    {
        DamageCalculator.OnCalculateDamage -= resultContainer.AddTotalDamage;
        Wallet.OnMoneyAdded -= resultContainer.AddTotalGetGold;
        Wallet.OnMoneyRemoved -= resultContainer.AddTotalSpendGold;
        ExpComponent.OnExpAdded -= resultContainer.AddTotalExp;
    }

    private void Start()
    {
        weaponCurator.ChooseFirstWeapons();
    }

    public void DisplayClearResultUI()
    {
        resultTitleText.text = "よく生き延びた！君の勝ちだ！";
        surviveWaveCntText.text = resultContainer.GetTotalSurviveWaveText();
        totalDefeatedEnemiesCntText.text = resultContainer.GetTotalDefeatedEnemiesText();
        totalDamageText.text = resultContainer.GetTotalDamageText();
        totalGetGoldText.text = resultContainer.GetTotalGetGoldText();
        totalSpendGoldText.text = resultContainer.GetTotalSpendGoldText();
        totalGetExpText.text = resultContainer.GetTotalGetExpText();
    }

    public void DisplayDefeatResultUI()
    {
        resultTitleText.text = "ああ・・・！死んでしまうとは情けない・・・";
        surviveWaveCntText.text = resultContainer.GetTotalSurviveWaveText();
        totalDefeatedEnemiesCntText.text = resultContainer.GetTotalDefeatedEnemiesText();
        totalDamageText.text = resultContainer.GetTotalDamageText();
        totalGetGoldText.text = resultContainer.GetTotalGetGoldText();
        totalSpendGoldText.text = resultContainer.GetTotalSpendGoldText();
        totalGetExpText.text = resultContainer.GetTotalGetExpText();
    }
}
