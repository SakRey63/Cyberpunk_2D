using TMPro;
using UnityEngine;

public class DisplayCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.WasFullWallet += ChangeNumber;
    }

    private void OnDisable()
    {
        _wallet.WasFullWallet -= ChangeNumber;
    }
    
    private void ChangeNumber(int number)
    {
        _text.text = number.ToString();
    }
}
