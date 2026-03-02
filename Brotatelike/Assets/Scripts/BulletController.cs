using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : PooledObject
{
    private WeaponData weaponData;

    private Vector3 velocity;
    private Vector3 startPos;

    [SerializeField] private string targetTag;

    public void Initialize(WeaponData data, Vector3 velocity)
    {
        weaponData = data;
        this.velocity = velocity;
    }

    protected override void OnSpawn()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if ((transform.position - startPos).sqrMagnitude > weaponData.range * weaponData.range) Release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            target.TakeDamage(weaponData.damage);

            Release();
        }
    }
}
