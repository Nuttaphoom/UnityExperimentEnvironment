using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

[CreateAssetMenu(fileName = "HealthStatSO", menuName = "ScriptableObject/CharacterStatSO/HealthStatSO")]
public class HealthStatSO : BaseCharacterSO
{
    [Header("Default Properties")]
    [SerializeField] private float _maxHP;
    private float _currentHP ; 
    public float MAXHP => _maxHP;
    public float CurrentHP => _currentHP;

    public override void Init()
    {
        _currentHP = _maxHP;
    }
    public override void ApplyTempIncreaseStat(TempIncreasingStat _stat)
    {
        _maxHP += _stat._maxHP;
    }


    public override void RemoveTempIncreaseStat(TempIncreasingStat _stat)
    {
        _maxHP -= _stat._maxHP;
    }

    public void RestoreHealth(float _volume)
    {
        
        _currentHP = ( _currentHP + _volume ) > _maxHP ? _maxHP : (_currentHP + _volume); 
    }

    public void ApplyDamage(float _dmg)
    {
        _currentHP -= _dmg;
        if (_currentHP < 0)
            _currentHP = 0;
        Debug.Log("current HP : " + _currentHP);
        Debug.Log("received dmg : " + _dmg);
    }

 
}
