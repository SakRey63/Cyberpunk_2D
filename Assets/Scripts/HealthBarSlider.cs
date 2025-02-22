using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private int _goodHealth = 70;
    private int _badHealth = 30;

    private void OnEnable()
    {
        _health.HealthPoint += ChangeHealthPlayer;
    }

    private void OnDisable()
    {
        _health.HealthPoint -= ChangeHealthPlayer;
    }

    private void ChangeColorHealthBar(float health)
    {
        _slider.fillRect.TryGetComponent(out Image image);
        
        if (health > _goodHealth)
        {
            image.color = Color.green;
        }
        else if(health > _badHealth)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.red;
        }
    }
    
    private void ChangeHealthPlayer(float health)
    {
        ChangeColorHealthBar(health);
        
        _slider.value = Mathf.MoveTowards(_slider.value, health, health);
    }
}
