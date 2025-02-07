using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> WasApplied;

    public void ApplyTreatment()
    {
        WasApplied?.Invoke(this);
    }
}
