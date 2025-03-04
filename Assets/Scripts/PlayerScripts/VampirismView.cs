using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Vampirism _vampirism;

    private float _maxValue = 100;

    private void OnEnable()
    {
        _vampirism.EnergyChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _vampirism.EnergyChanged -= ChangeValue;
    }

    private IEnumerator ChangeSmoothlyValue(float targetValue, float delay)
    {
        float tempPoint = _maxValue / delay;

        float epsilonTime = 0;
        
        while (epsilonTime < delay)
        {
            epsilonTime += Time.deltaTime;
            
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, tempPoint * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private void ChangeValue(float target, float delay)
    {
        StartCoroutine(ChangeSmoothlyValue(target, delay));
    }
}
