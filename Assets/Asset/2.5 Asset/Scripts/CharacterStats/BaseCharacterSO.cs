using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterSO : ScriptableObject
{
    [TextArea]
    private string Description = "This CharacterSO is served as stat container and/or manipualtor, and should be referenced from StatManager";
    public abstract void ApplyTempIncreaseStat(TempIncreasingStat _stat)  ; 
    public abstract void RemoveTempIncreaseStat(TempIncreasingStat _stat) ;

    public abstract void Init(); 
}

public struct TempIncreasingStat 
{
    public float _speed ;
    public float _maxHP ; 
}

 
