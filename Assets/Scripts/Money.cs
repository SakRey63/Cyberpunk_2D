using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action<Money> FoundMe;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
        {
            FoundMe?.Invoke(this);
        }
    }
}
