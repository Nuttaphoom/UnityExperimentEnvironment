using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

[CreateAssetMenu(fileName = "MovementStatSO", menuName = "ScriptableObject/CharacterStatSO/MovementStatSO")]
public class MovementStatSO : BaseCharacterSO
{
    [Header("Default Properties")]
    [SerializeField] private float _x_Axis_Movespeed;
    [SerializeField] private float _y_Axis_Movespeed;

    public float X_Axis_Movespeed => _x_Axis_Movespeed;
    public float Y_Axis_Movespeed => _y_Axis_Movespeed;

    public override void Init()
    {

    }
    public override void ApplyTempIncreaseStat(TempIncreasingStat _stat)
    {
        throw new System.NotImplementedException();
    }



    public override void RemoveTempIncreaseStat(TempIncreasingStat _stat)
    {
        throw new System.NotImplementedException();
    }
}