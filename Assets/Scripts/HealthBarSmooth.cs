using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmooth : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed = 70;

    private float _tempValue;
    
    private void OnEnable()
    {
        _health.HealthPoint += ChangeHealthPlayer;
    }

    private void OnDisable()
    {
        _health.HealthPoint -= ChangeHealthPlayer;
    }

    private void Update()
    {
        ChangeSmoothHealth();
    }

    private void ChangeHealthPlayer(float health)
    {
        _tempValue = health;
    }

    private void ChangeSmoothHealth()
    {
        if (_slider.value < _tempValue || _slider.value > _tempValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _tempValue, _speed * Time.deltaTime);
        }
    }
}
