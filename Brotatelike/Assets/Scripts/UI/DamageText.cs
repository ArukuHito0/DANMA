using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : PooledObject
{
    private TextMeshPro damageText;
    [SerializeField] private float animTime;
    [SerializeField] private float waitFadeTime;
    [SerializeField] private float fadeoutTime;

    private float damageCache;

    private Color defaultColor;

    protected override void OnSpawn()
    {
        damageText.color = defaultColor;
    }

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        defaultColor = damageText.color;
    }

    public void SetDamageText(int damage)
    {
        if (damage != damageCache)
        {
            damageText.SetText("{0}", damage);
            damageCache = damage;
        }

        StartAnim();
    }

    public void StartAnim()
    {
        StartCoroutine(DamageTextAnim());
    }

    private IEnumerator DamageTextAnim()
    {
        float time = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-1.3f, 1.3f));

        while (time < animTime)
        {
            time += Time.deltaTime;
            float t = time / animTime;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitFadeTime);

        time = 0;
        Color textColor = damageText.color;
        float startAlpha = textColor.a;

        while (time < fadeoutTime)
        {
            time += Time.deltaTime;
            float t = time / fadeoutTime;

            var alpha = Mathf.Lerp(startAlpha, 0, t);
            textColor.a = alpha;

            damageText.color = textColor;

            yield return null;
        }

        Release();
    }
}
