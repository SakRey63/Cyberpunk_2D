using UnityEngine;

public class UiButton : MonoBehaviour
{
    [SerializeField] private int _demage = 5;
    [SerializeField] private int _heal = 15;
    [SerializeField] private Health _health;

    public void DamagePlayer()
    {
        _health.TakeDamage(_demage);
    }

    public void HealingPlayer()
    {
        _health.Healing(_heal);
    }
}
