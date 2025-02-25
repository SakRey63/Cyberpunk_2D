using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmooth : HealthView
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _delay = 0.7f;

    protected override void ChangeValue(float health)
    {
        StartCoroutine(ChangeSmoothlyValue(health));
    }

    private IEnumerator ChangeSmoothlyValue(float health)
    {
        float elapsedTime = 0;

        float numberParts = health / _delay;
        
        while (elapsedTime < _delay)
        {
            elapsedTime += Time.deltaTime;
            
            _slider.value = Mathf.MoveTowards(_slider.value, health, numberParts * Time.deltaTime);
            
            ChangeColor(health);
            
            yield return null;
        }
    }
    
    private void ChangeColor(float health)
    {
        if (_slider.fillRect.TryGetComponent(out Image image))
        {
            image.color = base.CreateColor(health);
        }
    }
}