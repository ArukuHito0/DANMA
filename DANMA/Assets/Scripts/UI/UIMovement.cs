using System.Collections;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    private RectTransform r_transform;

    private Vector2 defaultPos;
    private Vector2 targetPos;
    [SerializeField] private Vector2 moveVector;

    [SerializeField] private float animationTime;

    private Coroutine activeAnim;

    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        r_transform = GetComponent<RectTransform>();
        defaultPos = r_transform.localPosition;
        targetPos = defaultPos + moveVector;
    }

    public void MoveToDefaultPos()
    {
        if (activeAnim != null)
            StopMoveAnim();

        activeAnim = StartCoroutine(MoveAnimation(defaultPos));
    }

    public void MoveToTargetPos()
    {
        if (activeAnim != null)
            StopMoveAnim();

        activeAnim = StartCoroutine(MoveAnimation(targetPos));
    }

    public void StopMoveAnim()
    {
        StopCoroutine(activeAnim);
        activeAnim = null;
    }

    public void SetDefaultPos()
    {
        if (activeAnim != null)
            StopMoveAnim();

        r_transform.localPosition = defaultPos;
    }

    public void SetTargetPos()
    {
        if (activeAnim != null)
            StopMoveAnim();

        r_transform.localPosition = targetPos;
    }

    private IEnumerator MoveAnimation(Vector3 targetPos)
    {
        float time = 0;
        Vector2 startPos = r_transform.localPosition;

        while (time < animationTime)
        {
            time += Time.unscaledDeltaTime;
            float t = time / animationTime;
            float curveT = easeCurve.Evaluate(t);

            r_transform.localPosition = Vector2.Lerp(startPos, targetPos, curveT);

            yield return null;
        }

        r_transform.localPosition = targetPos;
        activeAnim = null;
    }
}