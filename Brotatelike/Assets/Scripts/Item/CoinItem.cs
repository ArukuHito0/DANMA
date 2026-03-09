using System.Collections;
using UnityEngine;

public class CoinItem : PickableItem
{
    [SerializeField] private uint gold;

    [SerializeField] private float animTime;

    public override void PickUp(PlayerController player)
    {
        base.PickUp(player);

        if (gameObject.activeSelf)
            StartCoroutine(PickupAnim(player));
    }

    IEnumerator PickupAnim(PlayerController player)
    {
        float time = 0f;
        Vector3 startPos = transform.position;

        while (time < animTime && (player.transform.position - transform.position).sqrMagnitude > 0.1f)
        {
            time += Time.deltaTime;
            float t = time / animTime;
            t *= t;

            transform.position = Vector3.Lerp(startPos, player.transform.position, t);

            yield return null;
        }

        player.wallet.AddMoney(gold);

        Release();
    }
}
