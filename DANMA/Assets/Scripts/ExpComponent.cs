using System;
using System.Threading.Tasks;
using UnityEngine;

public class ExpComponent : MonoBehaviour
{
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;
    public long exp { get; private set; } = 0;
    public long levelUpExp { get; private set; } = 20;
    public float expRate
    {
        get
        {
            return (float)exp / levelUpExp;
        }
    }

    private int levelUpCnt = 0;

    public event Action<float> OnExpChanged;
    public static event Action<int> OnExpAdded;
    public event Action OnLevelChanged;

    private void Start()
    {
        OnExpChanged?.Invoke(0);
    }

    public void AddExp(int amount)
    {
        exp += amount;

        while (exp >= levelUpExp)
        {
            exp -= levelUpExp;
            LevelUp();
            levelUpExp = CurrentLevel * 10 + (long)((CurrentLevel * 0.1f * levelUpExp) * 0.2f);
        }

        OnExpAdded?.Invoke(amount);
        OnExpChanged?.Invoke(expRate);
    }

    public void LevelUp()
    {
        currentLevel++;
        levelUpCnt++;

        OnLevelChanged?.Invoke();
    }

    public void ResetLevelUpCount()
    {
        levelUpCnt = 0;
    }
}
