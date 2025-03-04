using System;
using UnityEngine;

public class PlayerScaner : MonoBehaviour
{ 
    private const string Money = "Money";
    private const string Medicine = "Medicine";
    
    [SerializeField] private Wallet _wallet;

    public event Action<Item> FoundTreatment;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.Name == Money)
            {
                _wallet.AddMoney();
                
                item.ApplyTreatment();
            }
            else if (item.Name == Medicine)
            {
                FoundTreatment?.Invoke(item);
            }
        }
    }
}