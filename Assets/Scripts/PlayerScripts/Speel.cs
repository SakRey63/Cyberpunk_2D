using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speel : MonoBehaviour
{
    [SerializeField] private float _power = 25;
    [SerializeField] private float _delay = 6;
    [SerializeField] private Slider _slider;

    private Enemy _target;
    private float _maxValue = 100;
    private float _minValue = 0;
    private float _distance;

    private List<Enemy> _enemies;
    
    public event Action Wasted;

    private void OnEnable()
    {
        _enemies = new List<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy)) 
        {
            _enemies.Add(enemy);
        }
    }
         
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        { 
            _enemies.Remove(enemy); 
        }
    }

    private IEnumerator Casting()
    {
        float epsilonTime = 0;

        while (epsilonTime < _delay)
        {
            epsilonTime += Time.deltaTime;
            
            SelectTarget();

            if (_target != null)
            {
                _target.TakeDamage(GetNumberDelta(_power) * Time.deltaTime);
            }
            
            _slider.value = Mathf.MoveTowards(_slider.value, _minValue, GetNumberDelta(_maxValue) * Time.deltaTime);
            
            yield return null;
        }
        
        Wasted?.Invoke();
        
        gameObject.SetActive(false);
    }
    
    public void Attack()
    {
        StartCoroutine(Casting());
    }

    private float GetNumberDelta(float number)
    {
        float numberPoint = number / _delay;

        return numberPoint;
    }

    private float GetDirection(Enemy enemy)
    {
        Vector3 playerPosition = gameObject.transform.position;
        
        _distance = (enemy.transform.position - playerPosition).sqrMagnitude;
        
        return _distance;
    }
    
    private void SelectTarget()
    {
        if (_enemies.Count > 0)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (_target != null)
                {
                    float tempDistance = GetDirection(enemy);

                    if (tempDistance < _distance)
                    {
                        _distance = tempDistance;

                        _target = enemy;
                    }
                }
                else
                {
                    _target = enemy;

                    _distance = GetDirection(enemy);
                }
            }
        }
        else
        {
            _target = null;
        }
    }
}