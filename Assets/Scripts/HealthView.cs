using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private float _goodHealth = 70;
    private float _badHealth = 30;
    
    private void OnEnable()
    {
        _health.ValueChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= ChangeValue;
    }

    protected abstract void ChangeValue(float health);
    
    protected Color CreateColor(float health)
    {
        if (health > _goodHealth)
        {
            return Color.green;
        }
        else if (health > _badHealth)
        {
            return Color.yellow;
        }
        else
        {
            return Color.red;
        }
    }
}