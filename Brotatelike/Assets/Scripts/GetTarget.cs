using UnityEngine;
using System.Collections.Generic;

public static class GetTarget
{
    public static T GetTargetInRange<T>(List<T> targetList, Vector3 ownerPos, float range) where T : MonoBehaviour
    {
        T _target = null;
        float minDistance = range * range;

        if (targetList.Count <= 0) return null;

        foreach (T target in targetList)
        {
            float sqrDist = (ownerPos - target.transform.position).sqrMagnitude;

            if (sqrDist <= minDistance)
            {
                minDistance = sqrDist;
                _target = target;
            }
        }

        return _target;
    }
}
