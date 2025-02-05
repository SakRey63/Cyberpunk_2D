using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action<Money> WasDiscovered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            WasDiscovered?.Invoke(this);
        }
    }
}
