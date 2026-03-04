using UnityEngine;
using System.Collections.Generic;

public static class GetTarget
{
    public static PlayerController GetPlayerInRange(Vector3 ownerPos, float range)
    {
        if(PlayerController.Instance == null) return null;

        float _range = range * range;

        float sqrDist = (PlayerController.Instance.transform.position - ownerPos).sqrMagnitude;

        if(sqrDist < _range)
        {
            return PlayerController.Instance;
        }
        else
        {
            return null;
        }
    }

    public static T GetTargetInRange<T>(List<T> targetList, Vector3 ownerPos, float range) where T : MonoBehaviour
    {
        T _target = null;
        float minDistance = range * range;

        if (targetList.Count <= 0) return null;

        foreach (T target in targetList.ToArray())
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

    public static void GetAllTargetinRange<T>(List<T> targetList, List<T> result, Vector3 ownerPos, float range) where T : MonoBehaviour
    {
        result?.Clear();
        float _range = range * range;

        if (targetList == null || targetList.Count <= 0) return;

        foreach (T target in targetList.ToArray())
        {
            float sqrDist = (ownerPos - target.transform.position).sqrMagnitude;

            if (sqrDist <= _range)
            {
                result.Add(target);
            }
        }
    }
}
