using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnMarker : PooledObject
{
    public static List<SpawnMarker> spawnMarkerList = new List<SpawnMarker>();

    [SerializeField] private Image spawnGauge;
    [SerializeField] private float spawnTime;

    private EnemyBase spawnEnemy;
    private float time;

    protected override void OnSpawn()
    {
        spawnMarkerList.Add(this);

        time = spawnTime;
    }

    public void SetEnemy(EnemyBase enemy) => spawnEnemy = enemy;

    private void Update()
    {
        time -= Time.deltaTime;

        spawnGauge.fillAmount = time / spawnTime;

        if (time < 0)
        {
            ObjectPoolManager.Instance.GetPooledObject(spawnEnemy, transform.position).Initialize();

            spawnMarkerList.Remove(this);
            Release();
        }
    }
}
