using UnityEngine;

public abstract class BulletBase : PooledObject
{
    protected WeaponData weaponData;

    protected Vector3 startPos;
    protected Vector3 velocity;

    [SerializeField] protected string targetTag;

    protected GameObject hitObj;
    protected IDamageable hitCache;

    protected abstract void OnHit();

    protected override void OnSpawn()
    {
        startPos = transform.position;
    }

    public void Initialize(WeaponData data, Vector3 velocity)
    {
        weaponData = data;
        this.velocity = velocity;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
        
        if((transform.position - startPos).sqrMagnitude > weaponData.Range * weaponData.Range)
            Release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if (collision.gameObject != hitObj || hitObj == null)
            {
                hitCache = collision.GetComponent<IDamageable>();
                hitObj = collision.gameObject;
            }

            OnHit();
        }
    }
}
