using TMPro;
using UnityEngine;

public class CounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.WasFullWallet += ChangeNumber;
    }

    private void OnDisable()
    {
        _wallet.WasFullWallet -= ChangeNumber;
    }
    
    private void ChangeNumber(float number)
    {
        _countMoney.text = number.ToString();
    }
}