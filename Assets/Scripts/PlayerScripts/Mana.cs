using System.Collections;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _amountMana = 100;
    [SerializeField] private Speel _speel;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeFilling = 4;

    private bool _isFill = false;

    private void OnEnable()
    {
        _speel.Wasted += RechargeSpell;
    }

    private void OnDisable()
    {
        _speel.Wasted -= RechargeSpell;
    }
    
    private void Start()
    {
        StartCoroutine(FillMana());
    }

    private IEnumerator FillMana()
    {
        float elapsedTime = 0;
        
        float numberDelta = _amountMana / _timeFilling;

        while (elapsedTime < _timeFilling)
        {
            elapsedTime += Time.deltaTime;

            _slider.value = Mathf.MoveTowards(_slider.value, _amountMana, numberDelta * Time.deltaTime);

            yield return null;
        }

        _isFill = true;
    }
    
    public void Casting()
    {
        if (_isFill)
        {
            _speel.gameObject.SetActive(true);
            _speel.Attack();
            
            _isFill = false;
        }
    }

    private void RechargeSpell()
    {
        StartCoroutine(FillMana());
    }
}