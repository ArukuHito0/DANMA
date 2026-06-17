using TMPro;
using UnityEngine;

public class CurrentMoneyText : MonoBehaviour
{
    private TextMeshProUGUI currentMoneyText;

    private void OnEnable()
    {
        Wallet.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        Wallet.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Awake()
    {
        currentMoneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //UpdateMoneyText(PlayerController.Instance.wallet.CurrentMoney);
    }

    private void UpdateMoneyText()
    {
        currentMoneyText.text = $"<sprite=8> {PlayerController.Instance.wallet.CurrentMoney}";
    }
}
