using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for manipulate states 
public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField] private HealthStatSO _healthStat ;
    [SerializeField] private MovementStatSO _movementStat ;

    private void Awake()
    {
        _healthStat.Init();
        _movementStat.Init(); 
    }
    public HealthStatSO HealthStat => _healthStat;
    public MovementStatSO MovementStat => _movementStat; 
}
