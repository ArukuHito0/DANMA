using System;
using System.Threading.Tasks;
using UnityEngine;

public class ExpComponent : MonoBehaviour
{
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;
    public int exp { get; private set; } = 0;
    public int levelUpExp { get; private set; } = 10;
    public float expRate
    {
        get
        {
            return (float)exp / levelUpExp;
        }
    }

    public event Action<float> OnExpChanged;
    public event Action OnLevelChanged;
    public event Action<string, bool> OnOpenUpgrade;

    private void Start()
    {
        OnExpChanged?.Invoke(0);
    }

    public void AddExp(int amount)
    {
        exp += amount;

        if (exp >= levelUpExp)
        {
            var e = exp - levelUpExp;
            exp = e <= 0 ? 0 : e;
            LevelUp();
            levelUpExp = CurrentLevel * 10 + (int)((CurrentLevel * 0.07f * levelUpExp) * 0.2f);

            if (exp != 0)
            {
                AddExp(exp);
            }
        }

        OnExpChanged?.Invoke(expRate);
    }

    public void LevelUp()
    {
        currentLevel++;

        OnLevelChanged?.Invoke();

        OnOpenUpgrade?.Invoke("UpgradeUI", true);
        OnOpenUpgrade?.Invoke("StatusUI", true);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Pause);
    }
}
