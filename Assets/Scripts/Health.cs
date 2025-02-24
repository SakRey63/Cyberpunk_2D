using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    private float _maxValue = 100;
    private float _dead = 0;
    private bool _isDead = false;
    private bool _isHeal = false;


    public bool IsDead => _isDead;
    public bool IsHeal => _isHeal;
    
    public event Action<float> ValueChanged;
    
    private void Start()
    {
        ValueChanged?.Invoke(_health);
    }
    
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        { 
            _health -= damage;
            
            if (_health <= _dead)
            {
                _health = _dead;
                        
                _isDead = true;
            }
        }
        
        ValueChanged?.Invoke(_health);
    }
    
    public void HealPlayer(float heal)
    {
        if (heal > 0)
        {
            if (_health < _maxValue && _isDead == false)
            {
                _health += heal;
            
                if (_health > _maxValue)
                {
                    _health = _maxValue;
                }

                _isHeal = true;
                
                ValueChanged?.Invoke(_health);
            }
        }
    }
    
    public void HealingIsOver()
    {
        _isHeal = false;
    }
}