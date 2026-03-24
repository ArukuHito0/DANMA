using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public abstract class EnemyBase : PooledObject
{
    public static List<EnemyBase> enemyList { get; private set; } = new List<EnemyBase>();

    [SerializeField] private EnemyStatusData _enemyStatusData;
    public EnemyStatusData enemyStatus { get => _enemyStatusData; private set => _enemyStatusData = value; }

    private HealthComponent healthComponent;

    [SerializeField] private EmitEffect defeatedEffect;

    protected Rigidbody2D rb;

    private Coroutine attackCoroutine = null;

    protected override void OnSpawn()
    {
        enemyList.Add(this);
    }

    public void Initialize()
    {
        healthComponent.SetHealthStats(enemyStatus.MaxHealth);
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        if (enemyList.Contains(this))
        {
            enemyList.Remove(this);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance != null)
        {
            Move();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Attack();
    }

    public void DropItems()
    {
        foreach (var config in enemyStatus.dropItemList)
        {
            ObjectPoolManager.Instance.GetPooledObject(
                config.itemPrefab, 
                transform.position + 
                new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 
                            UnityEngine.Random.Range(-0.5f, 0.5f)));
        }
    }

    public void SpawnDefeatedEffect() => ObjectPoolManager.Instance.GetPooledObject(defeatedEffect, transform.position);

    protected abstract void Attack();
    protected abstract void Move();
}
