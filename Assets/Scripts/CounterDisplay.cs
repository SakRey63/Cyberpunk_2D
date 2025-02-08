using TMPro;
using UnityEngine;

public class CounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private TextMeshProUGUI _healthPlayer;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Player _player;

    private int _goodHealth = 70;
    private int _badHealth = 30;

    private void OnEnable()
    {
        _wallet.WasFullWallet += ChangeNumber;
        _player.OnHeal += ChangeHealthPlayer;
        _player.OnDamage += ChangeHealthPlayer;
    }

    private void OnDisable()
    {
        _wallet.WasFullWallet -= ChangeNumber;
        _player.OnHeal -= ChangeHealthPlayer;
        _player.OnDamage -= ChangeHealthPlayer;
    }
    
    private void ChangeNumber(int number)
    {
        _countMoney.text = number.ToString();
    }

    private void ChangeHealthPlayer(int health)
    {
        if (health > _goodHealth)
        {
            _healthPlayer.color = Color.green;
        }
        else if(health > _badHealth)
        {
            _healthPlayer.color = Color.yellow;
        }
        else
        {
            _healthPlayer.color = Color.red;
        }
        
        _healthPlayer.text = health.ToString();
    }
}