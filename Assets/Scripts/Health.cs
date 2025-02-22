using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    private float _maxHealth = 100;
    private float _dead = 0;
    private bool _isDead = false;
    private bool _isHeal = false;

    public bool IsDead => _isDead;
    public bool IsHeal => _isHeal;
    
    public event Action<float> HealthPoint;

    private void Start()
    {
        HealthPoint?.Invoke(_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= _dead)
        {
            _health = _dead;
            
            _isDead = true;
        }
        
        HealthPoint?.Invoke(_health);
    }
    
    public void Healing(float heal)
    {
        if (_health < _maxHealth && heal > _dead && _isDead == false)
        {
            _isHeal = true;
            
            _health += heal;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            
            HealthPoint?.Invoke(_health);
        }
    }

    public void HealingIsOver()
    {
        _isHeal = false;
    }
}
