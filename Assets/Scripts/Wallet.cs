using System;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _counteMoney;
    [SerializeField] private TextMeshProUGUI _text;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Money money))
        {
            AddMoney();
            
            Destroy(money.gameObject);
        }
    }

    private void AddMoney()
    {
        _counteMoney++;
        
        ReflectAmountMoney(_counteMoney);
    }
    
    private void ReflectAmountMoney(int countMoney)
    {
        string money = Convert.ToString(countMoney);

        _text.SetText(money);
    }
}
