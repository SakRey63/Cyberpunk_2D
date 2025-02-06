using TMPro;
using UnityEngine;

public class DisplayCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private TextMeshProUGUI _healthPlayer;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private RefereeGame _referee;

    private void OnEnable()
    {
        _wallet.WasFullWallet += ChangeNumber;
        _referee.ChangeHealthPlayer += ChangeHealth;

    }

    private void OnDisable()
    {
        _wallet.WasFullWallet -= ChangeNumber;
        _referee.ChangeHealthPlayer -= ChangeHealth;
    }
    
    private void ChangeNumber(int number)
    {
        _countMoney.text = number.ToString();
    }

    private void ChangeHealth(int health)
    {
        _healthPlayer.text = health.ToString();
    }
}