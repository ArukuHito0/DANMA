using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private ShopCurator shopCurator;
    [SerializeField] private TextMeshProUGUI waveCntText;
    [SerializeField] private TextMeshProUGUI rerollText;

    private void OnEnable()
    {
        shopCurator.OnShopEnter += UpdateWaveCntText;
        shopCurator.OnRerolled += UpdateRerollText;
    }

    private void OnDisable()
    {
        shopCurator.OnShopEnter -= UpdateWaveCntText;
        shopCurator.OnRerolled -= UpdateRerollText;
    }

    private void UpdateWaveCntText()
    {
        waveCntText.SetText("WAVE {0}", EnemyGenerator.Instance.currentWaveCnt);
    }

    private void UpdateRerollText(int price)
    {
        rerollText.SetText("ÉäÉçÅ[Éã - <sprite=8>{0}", price);
    }
}
