using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator Instance {  get; private set; }

    [SerializeField] private List<WaveData> waveDataList;

    [SerializeField] private Transform fieldSize;
    [SerializeField] private float margin;

    [SerializeField] private TextMeshProUGUI waveCntText;

    [SerializeField] private SpawnMarker spawnMarker;

    private float spawnRangeX;
    private float spawnRangeY;

    public int currentWaveCnt { get; private set; } = 0;
    public int waveIdx => (int)Mathf.Clamp(currentWaveCnt - 1, 0, waveDataList.Count);
    private WaveData CurrentWave => waveDataList[waveIdx];

    public event Action<int> OnUpdateWaveTime;

    [SerializeField] private UnityEvent OnStartWave;
    [SerializeField] private UnityEvent OnEndWave;
    [SerializeField] private UnityEvent OnEndFinalWave;

    private Coroutine activeWave;

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        Instance = this;

        spawnRangeX = fieldSize.localScale.x * 0.5f - margin;
        spawnRangeY = fieldSize.localScale.y * 0.5f - margin;
    }

    public void StartWave()
    {
        currentWaveCnt = Mathf.Clamp(currentWaveCnt + 1, 1, waveDataList.Count);

        if (activeWave != null)
        {
            StopCoroutine(activeWave);
            activeWave = null;
        }

        activeWave = StartCoroutine(EnemyGenerate(CurrentWave));
        StartCoroutine(WaveTimer(CurrentWave.waveTime));

        if (waveCntText != null) waveCntText.text = $"WAVE {currentWaveCnt}";

        OnStartWave?.Invoke();
    }

    private void StopWave()
    {
        StopCoroutine(activeWave);

        if (currentWaveCnt >= waveDataList.Count)
            OnEndFinalWave?.Invoke();
        else
            OnEndWave?.Invoke();
    }

    private IEnumerator EnemyGenerate(WaveData wave)
    {
        foreach (WaveData.SpawnData spawnData in wave.spawns)
        {
            spawnData.SetSpawnTime(0);
        }

        while (true)
        {
            foreach (WaveData.SpawnData spawnData in wave.spawns)
            {
                if (Time.time - spawnData.spawnTime >= spawnData.spawnInterbal)
                {
                    spawnData.SetSpawnTime(Time.time);

                    for (int i = 0; i < spawnData.spawnCnt * (1 + PlayerStatus.EnemySpawnRate.GetRuntimeStatus() * 0.01f); i++)
                    {
                        SpawnEnemy(spawnData.enemy);
                        // 5‘Ì–ˆ‚É1ƒtƒŒ[ƒ€‘Ò‹@
                        if (i % 5 == 0) yield return null;
                    }
                }
            }

            yield return null;
        }        
    }

    private IEnumerator WaveTimer(float waveTime)
    {
        int time = (int)waveTime;

        while (time > 0)
        {
            OnUpdateWaveTime?.Invoke(time);
            yield return new WaitForSeconds(1);
            time -= 1;
        }

        StopWave();

        yield break;
    }

    private void SpawnEnemy(EnemyBase enemy)
    {
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnRangeX, spawnRangeX), UnityEngine.Random.Range(-spawnRangeY, spawnRangeY));

        ObjectPoolManager.Instance.GetPooledObject(spawnMarker, spawnPos).SetEnemy(enemy);
    }

    public void ReleaseAllEnemy()
    {
        for(int i = EnemyBase.enemyList.Count - 1; i >= 0; i--)
            EnemyBase.enemyList[i].Release();

        for(int i = SpawnMarker.spawnMarkerList.Count - 1;i >= 0;i--)
            SpawnMarker.spawnMarkerList[i].Release();
    }
}
