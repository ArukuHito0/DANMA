using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCard : MonoBehaviour
{
    public ProductBaseData productData {  get; private set; }

    [SerializeField] private Sprite lockImage;
    [SerializeField] private Sprite unlockImage;

    [SerializeField] private GameObject itemCard;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;

    public event Action<ProductBaseData> OnPayItem;

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        itemFrame.color = productData.Tier.GetTierColor();
        itemIcon.sprite = productData.Icon;
        itemName.text = productData.Name;
        itemEffect.text = productData.GetDescriptionText();
        itemPrice.text = productData.Price.ToString() + " G";
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetItemLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetProductData(ProductBaseData data)
    {
        if (isLocked) return;

        this.productData = data;
    }

    public void PayProduct()
    {
        productData.PayProduct();

        isPayied = true;
        isLocked = false;

        OnPayItem?.Invoke(productData);

        itemCard.SetActive(false);
    }
}
