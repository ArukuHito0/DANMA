using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    private ItemData itemData;

    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI cnttext;

    public void Initialize(ItemData data, int cnt)
    {
        if (itemData == null) itemData = data;

        productIcon.sprite = data.Icon;
        cnttext.text = cnt.ToString();
    }
}
