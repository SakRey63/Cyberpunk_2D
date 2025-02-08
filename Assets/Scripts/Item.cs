using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    
    private int _heal = 25;
    
    public string Name => _name;
    public int Heal => _heal;
    
    public event Action<Item> Applied;

    public void ApplyTreatment()
    {
        Applied?.Invoke(this);
    }
}
