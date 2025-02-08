using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private int _maxHealth = 100;
    private int _dead = 0;
    private bool _isDead = false;
    private bool _isHeal = false;

    public bool IsDead => _isDead;
    public bool IsHeal => _isHeal;
    public int HealthCount => _health;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= _dead)
        {
            _isDead = true;
        }
    }
    
    public void Healing(int heal)
    {
        if (_health < _maxHealth)
        {
            _isHeal = true;
            
            _health += heal;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    public void HealingIsOver()
    {
        _isHeal = false;
    }
}
