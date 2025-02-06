using System;
using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private int _maxHealthe = 100;
    [SerializeField] private int _heal = 25;

    public event Action<MedicineChest> WasApplied;

    public int Heal => _heal;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.Health < _maxHealthe)
            {
                WasApplied?.Invoke(this);
            }
        }
    }
}
