using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceItem : PickableItem
{
    [SerializeField] private int experiencePoint;

    [SerializeField] private float animTime;

    public override void PickUp(PlayerController player)
    {
        base.PickUp(player);

        if(gameObject.activeSelf)
            StartCoroutine(PickupAnim(player));
    }

    IEnumerator PickupAnim(PlayerController player)
    {
        float time = 0f;
        Vector3 startPos = transform.position;

        while (time < animTime)
        {
            time += Time.deltaTime;
            float t = time / animTime;
            t *= t;

            transform.position = Vector3.Lerp(startPos, player.transform.position, t);

            yield return null;
        }

        player.ExpComponent.AddExp(experiencePoint);

        Release();
    }
}
