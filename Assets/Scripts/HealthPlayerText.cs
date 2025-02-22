using TMPro;
using UnityEngine;

public class HealthPlayerText : MonoBehaviour
{
    private const string DirectSlash = " / ";
    
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private Health _health;

    private int _goodHealth = 70;
    private int _badHealth = 30;
    private int _maxHealth = 100;
    
    private void OnEnable()
    {
        _health.HealthPoint += BringPlayerHealth;
    }

    private void OnDisable()
    {
        _health.HealthPoint -= BringPlayerHealth;
    }

    private void Start()
    {
        BringMaxHealth();
    }

    private void ChangeColorTextHealth(TextMeshProUGUI text, float health)
    {
        if (health > _goodHealth)
        {
            text.color = Color.green;
        }
        else if(health > _badHealth)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.red;
        }
    }

    private void BringPlayerHealth(float health)
    {
        ChangeColorTextHealth(_playerHealthText, health);

        _playerHealthText.text = health.ToString();
    }

    private void BringMaxHealth()
    {
        ChangeColorTextHealth(_maxHealthText, _maxHealth);
        
        _maxHealthText.text = DirectSlash + _maxHealth.ToString();
    }
}
