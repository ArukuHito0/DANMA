using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCard : MonoBehaviour
{
    public IProduct product {  get; private set; }

    [SerializeField] private AudioClip sellSe;
    [SerializeField] private AudioClip sellFailedSe;

    [SerializeField] private Sprite lockImage;
    [SerializeField] private Sprite unlockImage;

    [SerializeField] private Image iconBackground;
    [SerializeField] private Image productFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productEffect;
    [SerializeField] private Image priceLabel;
    [SerializeField] private TextMeshProUGUI productPrice;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;
    public bool isSale { get; private set; } = false;

    private int price = 0;

    private void OnEnable()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += UpdateCardVisual;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= UpdateCardVisual;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        UpdateCardVisual();
    }

    public void UpdateCardVisual()
    {
        Color color = product.Tier.GetTierColor();
        iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
        productFrame.color = color;
        productIcon.sprite = product.Icon;
        productName.text = product.Name;
        productEffect.text = product.GetDescriptionText();
        productPrice.SetText("{0}", price);
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";

        if (PlayerController.Instance.wallet.CanBuy(price))
        {
            // セール時は緑色で価格を表示
            if(isSale)
                productPrice.color = Color.green;
            else
                productPrice.color = Color.white;
        }
        else
        {
            productPrice.color = Color.red;
        }
    }

    public void SetProductLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetProduct(IProduct data)
    {
        if (isLocked) return;

        this.product = data;

        // 商品をセールにするか
        if (UnityEngine.Random.value < PlayerController.Instance.playerRuntimeStatus.SaleSpawnChance * 0.01f)
            isSale = true;
        else
            isSale = false;

        price = PriceCalculate(data.Price);
    }

    public void PayProduct()
    {
        if (!product.CanBuy())
        {
            Debug.Log("購入条件を満たしていません");
            SoundUtil.PlaySe(sellFailedSe.name);
            return;
        }

        SoundUtil.PlaySe(sellSe.name);

        PlayerController.Instance.wallet.RemoveMoney(price);

        product?.PayProduct();

        isPayied = true;
        isLocked = false;

        priceLabel.rectTransform.localScale = new Vector3(1, 1, 1);

        gameObject.SetActive(false);
    }

    // 価格計算
    private int PriceCalculate(float basePrice)
    {
        basePrice -= basePrice * (PlayerController.Instance.playerRuntimeStatus.ItemPriceRate * 0.01f);

        if (isSale)
            basePrice *= 0.5f;

        return (int)basePrice;
    }
}
