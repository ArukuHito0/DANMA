using System.Collections;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    private RectTransform r_transform;

    [SerializeField] private float animationTime;
    private Vector3 defaultScale;
    private Vector3 targetScale;
    [SerializeField] private float scaler;

    private Coroutine activeAnim;

    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        r_transform = GetComponent<RectTransform>();
        defaultScale = r_transform.localScale;
        targetScale = new Vector3(defaultScale.x * scaler, defaultScale.y * scaler);
    }
    
    public void ScaleToDefault()
    {
        if (activeAnim != null)
            StopScaleAnim();

        activeAnim = StartCoroutine(ScaleAnimation(defaultScale));
    }

    public void ScaleToTarget()
    {
        if (activeAnim != null)
            StopScaleAnim();

        activeAnim = StartCoroutine(ScaleAnimation(targetScale));
    }

    public void StopScaleAnim()
    {
        StopCoroutine(activeAnim);
        activeAnim = null;
    }

    public void SetDefaultScale()
    {
        if (activeAnim != null)
            StopScaleAnim();

        r_transform.localScale = defaultScale;
    }

    public void SetTargetScale()
    {
        if (activeAnim != null)
            StopScaleAnim();

        r_transform.localScale = targetScale;
    }

    private IEnumerator ScaleAnimation(Vector3 targetScale)
    {
        float time = 0;
        Vector3 startScale = r_transform.localScale;

        while (time < animationTime)
        {
            time += Time.unscaledDeltaTime;
            float t = easeCurve.Evaluate(time / animationTime);

            r_transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        r_transform.localScale = targetScale;
        activeAnim = null;
    }
}
