using ObjectPoolSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private ObjectPool enemyPool;

    [SerializeField] private List<WaveData> waveDataList;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private float margin;

    private float spawnRangeX;
    private float spawnRangeY;

    public uint currentWaveCnt { get; private set; } = 1;
    public int waveIdx => currentWaveCnt - 1 < 0 ? 1 : (int)currentWaveCnt - 1;

    public event Action<int> OnUpdateWaveTime;
    public event Action OnEndWave;

    private void OnEnable()
    {
        ShopManager sm = FindObjectOfType<ShopManager>();
        if(sm != null)
            sm.OnEndShopping += StartWave;
    }

    private void OnDisable()
    {
        ShopManager sm = FindObjectOfType<ShopManager>();
        if(sm != null)
            sm.OnEndShopping -= StartWave;
    }

    private void Awake()
    {
        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();

        spawnRangeX = fieldSize.localScale.x * 0.5f - margin;
        spawnRangeY = fieldSize.localScale.y * 0.5f - margin;
    }

    private void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        StartCoroutine(SpawnEnemy(waveDataList[waveIdx]));
    }

    private IEnumerator SpawnEnemy(WaveData wave)
    {
        foreach (WaveData.SpawnData spawnData in wave.spawns)
        {
            spawnData.SetSpawnTime(0);
        }

        float time = wave.waveTime;

        while (time >= 0)
        {
            foreach (WaveData.SpawnData spawnData in wave.spawns)
            {
                if (Time.time - spawnData.spawnTime >= spawnData.spawnInterbal)
                {
                    spawnData.SetSpawnTime(Time.time);

                    for (int i = 0; i < spawnData.spawnCnt; i++)
                        EnemyGenerate(spawnData.enemy);
                }
            }

            time -= Time.deltaTime;
            OnUpdateWaveTime?.Invoke((int)time);

            yield return null;
        }

        OnEndWave?.Invoke();
        ReleaseAllEnemy();
    }

    private void EnemyGenerate(EnemyStatusData enemyStatusData)
    {
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnRangeX, spawnRangeX), UnityEngine.Random.Range(-spawnRangeY, spawnRangeY));

        EnemyBase enemy = enemyPool.GetPooledObject(spawnPos).GetComponent<EnemyBase>();
        enemy.Initialize(enemyStatusData);
    }

    private void ReleaseAllEnemy()
    {
        var list = EnemyBase.enemyList.ToList();

        foreach (EnemyBase enemy in list)
        {
            enemy.Release();
        }

        EnemyBase.enemyList.Clear();
    }
}
