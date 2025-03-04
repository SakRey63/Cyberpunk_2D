using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismArea _area;
    [SerializeField] private Health _health;
    [SerializeField] private float _vampirismForse = 25;
    [SerializeField] private float _absorbEnergyMax = 100;
    [SerializeField] private float _absorbEnergyMin = 0;
    [SerializeField] private float _reloadTime = 4;
    [SerializeField] private float _absorbTime = 6;
    
    private Coroutine _coroutine;
    
    public event Action<float, float> EnergyChanged;

    private void Start()
    {
        _coroutine = StartCoroutine(RechargeVamp());
    }

    private IEnumerator CastingVamp()
    { 
        float epsilonTime = 0;
        
        EnergyChanged?.Invoke(_absorbEnergyMin, _absorbTime);

        float numberDelta = _vampirismForse / _absorbTime;

        while (epsilonTime < _absorbTime)
        {
            epsilonTime += Time.deltaTime;
            
            if (_area.Index > 0)
            {
                Enemy enemy = _area.SelectTarget();
                
                enemy.TakeDamage(numberDelta * Time.deltaTime);
                
                _health.Heal(numberDelta * Time.deltaTime);
                
                _health.UseHealing();
            }
            
            yield return null;
        }
        
        _area.gameObject.SetActive(false);

        StartCoroutine(RechargeVamp());
    }

    private IEnumerator RechargeVamp()
    {
        float epsilonTime = 0;
        
        EnergyChanged?.Invoke(_absorbEnergyMax, _reloadTime);
        
        while (epsilonTime < _reloadTime)
        {
            epsilonTime += Time.deltaTime;
            
            yield return null;
        }

        _coroutine = null;
    }

    public void UseVampirism()
    {
        if (_coroutine == null)
        {
            _area.gameObject.SetActive(true);
            
            _coroutine = StartCoroutine(CastingVamp());
        }
    }
}