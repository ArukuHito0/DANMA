using ObjectPoolSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class EnemyBase : PooledObject
{
    public static List<EnemyBase> enemyList = new List<EnemyBase>();

    protected EnemyRuntimeStatus status;

    private ObjectPool expPool;

    private HealthComponent healthComponent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float power;

    private Coroutine attackCoroutine;

    protected override void OnSpawn()
    {
        enemyList.Add(this);

        if (status.EnemyStatusData)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine(PlayerController.Instance.HealthComponent));
        }
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

    private void OnDestroy()
    {
        healthComponent.OnDead -= Release;
        healthComponent.OnDead -= () => expPool.GetPooledObject().transform.position = transform.position;
    }

    private void Awake()
    {
        status = GetComponent<EnemyRuntimeStatus>();
        expPool = GameObject.Find("ExpPool").GetComponent<ObjectPool>();

        healthComponent = GetComponent<HealthComponent>();

        healthComponent.OnDead += Release;
        healthComponent.OnDead += () => expPool.GetPooledObject().transform.position = transform.position;
    }

    private void Start()
    {
        if (status.EnemyStatusData)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine(PlayerController.Instance.HealthComponent));
        }
    }

    private void Update()
    {
        if (PlayerController.Instance != null)
        {
            transform.position += (PlayerController.Instance.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
    }

    public virtual IEnumerator AttackCoroutine(HealthComponent target) { yield return null; }
}
