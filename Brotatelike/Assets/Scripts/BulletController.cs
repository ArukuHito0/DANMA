using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : PooledObject
{
    private float damage;
    private float moveSpeed;
    private Vector3 targetDirection;

    [SerializeField] private string targetTag;

    public void Initialize(Vector3 dir, float spd, float dmg)
    {
        targetDirection = dir;
        moveSpeed = spd;
        damage = dmg;
    }

    protected override void OnSpawn()
    {
        Invoke(nameof(Release), 2f);
    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += targetDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            CancelInvoke(nameof(Release));

            IDamageable target = collision.GetComponent<IDamageable>();
            target.TakeDamage(damage);

            Release();
        }
    }
}
