using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    // プレイヤーのステータス系
    [SerializeField] private PlayerStatusData playerStatus;
    public PlayerStatusData PlayerStatus => playerStatus;
    public PlayerRuntimeStatus playerRuntimeStatus { get; private set; } = new PlayerRuntimeStatus();
    public Wallet wallet { get; private set; } = new Wallet();

    // 装備のインベントリ
    public static WeaponInventory weaponInventory { get; private set; }
    public static ItemInventory itemInventory { get; private set; }

    // 体力
    private HealthComponent healthComponent;
    public HealthComponent HealthComponent => healthComponent;

    // 経験値
    private ExpComponent expComponent;
    public ExpComponent ExpComponent => expComponent;

    public bool IsDead => healthComponent.IsDead;

    private Animator animator;

    [SerializeField] private Transform fieldSize;
    [SerializeField] private LayerMask targetLayer;

    private Vector3 moveDir = Vector3.zero;

    private bool canMoving = true;

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;

        Instance = this;

        healthComponent = GetComponent<HealthComponent>();
        expComponent = GetComponent<ExpComponent>();
        animator = GetComponent<Animator>();

        weaponInventory = new WeaponInventory();
        itemInventory = new ItemInventory();
    }

    private void Start()
    {
        healthComponent.SetHealthStats(playerRuntimeStatus.MaxHealth);
        healthComponent.SetDodgeChance(playerRuntimeStatus.DodgeChance);
        healthComponent.SetArmor(playerRuntimeStatus.Armor);

        wallet.SetMoney(playerStatus.firstGold);
    }

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0).normalized;

        if (canMoving)
        {
            if (moveDir != Vector3.zero)
            {
                animator.SetBool("Move", true);

                var pos = transform.position + moveDir * playerRuntimeStatus.MoveSpeed * Time.deltaTime;
                pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 0.5f, fieldSize.localScale.x * 0.5f - 0.5f);
                pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 0.5f, fieldSize.localScale.y * 0.5f - 0.5f);
                transform.position = pos;
            }
            else
            {
                animator.SetBool("Move", false);
            }
        }
    }

    public void StartAllWeaponCoroutine()
    {
        if (!weaponInventory.HasAnyWeapon()) return;

        foreach (var weapon in weaponInventory.weaponList)
        {
            if(weapon.GetWeaponData() == null) continue;

            Debug.Log($"{weapon.GetWeaponData().Name}の攻撃サイクルを開始");
            weapon.StartAttack(this);
        }
    }

    public void StopAllWeapopnCoroutine()
    {
        if (!weaponInventory.HasAnyWeapon()) return;

        foreach (var weapon in weaponInventory.weaponList)
        {
            if (weapon.GetWeaponData() == null) continue;

            Debug.Log($"{weapon.GetWeaponData().Name}の攻撃サイクルを停止");
            weapon.StopAttack(this);
        }
    }

    public void PlayerFixed()
    {
        canMoving = false;
    }

    public void PlayerMovable()
    {
        canMoving= true;
    }

    public void ResetPlayerPos()
    {
        transform.position = Vector3.zero;
    }

    public void OnEndWaveAct()
    {
        healthComponent.Heal(playerRuntimeStatus.MaxHealth * (playerRuntimeStatus.WaveHeal * 0.01f));
        wallet.AddMoney((int)playerRuntimeStatus.WaveGetGold);
    }
}
