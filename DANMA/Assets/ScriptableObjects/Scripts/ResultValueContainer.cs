using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "ResultValueContainer", menuName = "ResultValueContainer")]
public class ResultValueContainer : ScriptableObject
{
    private int totalSurviveWaveCnt;
    private int totalDefeatedEnemies;
    private float totalDamage;
    private long totalGetGold;
    private long totalSpendGold;
    private int totalGetExp;

    public void SetTotalWaveCnt()
    {
        totalSurviveWaveCnt = EnemyGenerator.Instance.currentWaveCnt;
    }

    public void AddDefeatedEnemiesCnt()
    {
        totalDefeatedEnemies++;
    }

    public void AddTotalDamage(float damage)
    {
        totalDamage += damage;
    }

    public void AddTotalGetGold(int gold)
    {
        totalGetGold += gold;
    }

    public void AddTotalSpendGold(int spend)
    {
        totalSpendGold += spend;
    }

    public void AddTotalExp(int exp)
    {
        totalGetExp += exp;
    }

    public string GetTotalSurviveWaveText() => $"{totalSurviveWaveCnt} WAVES";
    public string GetTotalDefeatedEnemiesText() => $"{totalDefeatedEnemies} ‘Ì";
    public string GetTotalDamageText() => $"{totalDamage.ToString("N0")}";
    public string GetTotalGetGoldText() => $"<sprite=8>{totalGetGold.ToString("N0")}";
    public string GetTotalSpendGoldText() => $"<sprite=8>{totalSpendGold.ToString("N0")}";
    public string GetTotalGetExpText() => $"{totalGetExp.ToString("N0")}";

    public void ResetData()
    {
        totalSurviveWaveCnt = 0;
        totalDefeatedEnemies = 0;
        totalDamage = 0;
        totalGetGold = 0;
        totalSpendGold = 0;
        totalGetExp = 0;
    }
}
