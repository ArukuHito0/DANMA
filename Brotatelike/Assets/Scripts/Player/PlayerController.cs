using Mono.Cecil.Cil;
using ObjectPoolSystem;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    private ObjectPool bulletPool;

    private HealthComponent healthComponent;
    public HealthComponent HealthComponent => healthComponent;
    private ExpComponent expComponent;
    public ExpComponent ExpComponent => expComponent;

    public bool IsDead => healthComponent.IsDead;

    [SerializeField] private float moveSpeed;
    private float defaultMoveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float power;
    [SerializeField] private float range;
    public float Range => range;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;

    [SerializeField] private float collectRange;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private LayerMask targetLayer;

    private Vector3 moveDir = Vector3.zero;

    private void OnDisable()
    {
        EnemyBase.target = null;

        healthComponent.OnDead -= () => gameObject.SetActive(false);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        EnemyBase.target = this;
        BulletController.player = this;

        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDead += () => gameObject.SetActive(false);

        expComponent = GetComponent<ExpComponent>();
        defaultMoveSpeed = moveSpeed;
    }

    private void Start()
    {
        StartCoroutine(Shooter());
        StartCoroutine(CollectItem());
    }

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0).normalized;

        var pos = transform.position + moveDir * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 1, fieldSize.localScale.x * 0.5f - 1);
        pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 1, fieldSize.localScale.y * 0.5f - 1);
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.E))
            healthComponent.TakeDamage(10);
        else if(Input.GetKeyDown(KeyCode.R))
            healthComponent.Heal(10);
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            EnemyBase target = GetTarget.GetTargetInRange(EnemyBase.enemyList, transform.position, range);

            if (target != null)
            {
                BulletController bullet = bulletPool.GetPooledObject().GetComponent<BulletController>();
                bullet.transform.position = transform.position;
                bullet.Initialize((target.transform.position - bullet.transform.position).normalized, bulletSpeed, power);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator CollectItem()
    {
        while (true)
        {
            GetTarget.GetTargetInRange(PickableItem.itemList, transform.position, collectRange)?.PickUp(this);

            yield return null;
        }
    }

    public void AddPower(int amount)
    {
        power += amount;
    }

    public void AddMoveSpeed(float amount)
    {
        moveSpeed += float.Parse((defaultMoveSpeed * (amount / 100)).ToString("F1"));
    }
}
