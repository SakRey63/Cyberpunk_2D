using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _amountMana = 100;
    [FormerlySerializedAs("_speel")] [SerializeField] private Vampirism vampirism;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _timeFilling = 4;

    private bool _isFill = false;

    private void OnEnable()
    {
        vampirism.Wasted += RechargeSpell;
    }

    private void OnDisable()
    {
        vampirism.Wasted -= RechargeSpell;
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
            vampirism.gameObject.SetActive(true);
            vampirism.Attack();
            
            _isFill = false;
        }
    }

    private void RechargeSpell()
    {
        StartCoroutine(FillMana());
    }
}