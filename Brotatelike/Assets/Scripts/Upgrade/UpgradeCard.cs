using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private PlayerController player;

    private RectTransform upgradeCard;
    private Vector3 defalutScale;
    private Vector3 pickupScale;

    [SerializeField] private UpgradeBaseData upgrade;

    [SerializeField] private float scaleUpValue;
    [SerializeField] private float animationTime;

    [SerializeField] private Image upgradeFrame;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeEffect;
    [SerializeField] private TextMeshProUGUI upgradeEffectValue;

    public static event Action<string, bool> OnCloseUpgrade;

    private Coroutine activeAnim;

    private void OnEnable()
    {
        upgradeFrame.color = upgrade.GetUpgradeColor();
        upgradeName.text = upgrade.upgradeName;
        upgradeEffect.text = upgrade.GetEffectName();
        upgradeEffectValue.text = upgrade.GetEffectValue();

        upgradeCard.localScale = defalutScale;
    }

    private void OnDisable()
    {
        activeAnim = null;
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        upgradeCard = GetComponent<RectTransform>();
        defalutScale = upgradeCard.localScale;
        pickupScale = new Vector3(upgradeCard.localScale.x * scaleUpValue, upgradeCard.localScale.y * scaleUpValue);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activeAnim != null)
            StopCoroutine(activeAnim);

        upgradeCard.localScale = pickupScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayCardAnim(ScaleAnimation(defalutScale));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCloseUpgrade?.Invoke("UpgradeUI", false);
        OnCloseUpgrade?.Invoke("StatusUI", false);

        upgrade.Upgrade(player);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Normal);
    }

    private void PlayCardAnim(IEnumerator anim)
    {
        if(activeAnim != null)
            StopCoroutine(activeAnim);

        activeAnim = StartCoroutine(anim);
    }

    IEnumerator ScaleAnimation(Vector3 targetScale)
    {
        float time = 0;
        Vector3 startScale = upgradeCard.localScale;

        while (time < animationTime)
        {
            time += Time.unscaledDeltaTime;
            float t = time / animationTime;

            upgradeCard.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        upgradeCard.localScale = targetScale;
        activeAnim = null;
    }
}
