using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class UIScaler : MonoBehaviour
{
    private RectTransform r_transform;

    [SerializeField] private float animationTime;
    [SerializeField] private float scalerRange;

    public Vector3 defaultScale { get; private set; }
    public Vector3 targetScale { get; private set; }

    private Coroutine activeAnim;

    private void Awake()
    {
        r_transform = GetComponent<RectTransform>();
        defaultScale = r_transform.localScale;
        targetScale = new Vector3(r_transform.localScale.x * scalerRange, r_transform.localScale.y * scalerRange);
    }

    public void ScaleToTarget()
    {
        if (activeAnim != null)
            StopScaleAnim();

        activeAnim = StartCoroutine(ScaleAnimation(targetScale));
    }

    public void ScaleToDefault()
    {
        if (activeAnim != null)
            StopScaleAnim();

        activeAnim = StartCoroutine(ScaleAnimation(defaultScale));
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
            float t = time / animationTime;

            r_transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        r_transform.localScale = targetScale;
        activeAnim = null;
    }
}
