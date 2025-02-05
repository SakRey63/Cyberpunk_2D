using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _counteMoney;

    public event Action<int> WasFullWallet;

    public void AddMoney()
    {
        _counteMoney++;
        
        WasFullWallet?.Invoke(_counteMoney);
    }
}
