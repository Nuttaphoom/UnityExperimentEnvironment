using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIHPBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] BasicCharacterController _basicCharacterController ;

    private HealthStatSO _healthStateSO; 
 
    private void Awake()
    {
       _healthStateSO = _basicCharacterController.gameObject.GetComponent<CharacterStatsManager>().HealthStat;
    }

    private void OnEnable()
    {
        _basicCharacterController.DamageEvent += OnDamage;

    }

    private void OnDisable()
    {
        _basicCharacterController.DamageEvent -= OnDamage;

    }

    private void OnDamage(float _dmg)
    {
        UpdateHPBar(); 

    }

    private void UpdateHPBar()
    {
        _slider.maxValue = _healthStateSO.MAXHP;
        _slider.value = _healthStateSO.CurrentHP ;
    }
}
