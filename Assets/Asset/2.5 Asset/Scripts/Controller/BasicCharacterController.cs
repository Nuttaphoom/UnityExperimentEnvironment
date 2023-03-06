using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterStatsManager))]
public class BasicCharacterController : MonoBehaviour
{
    private CharacterStatsManager _characterStatsManager; 
    private CharacterController _characterController;
    private BasicCharacterBrain _brain;
    private int _faceingDirection = 1;  //1 = right, -1 = left 

    //OnDamage event, listen by Brain , HP Bar and boardcast by HurtHandler
    public UnityAction<float> DamageEvent;

 

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _brain = GetComponent<BasicCharacterBrain>();
        _characterStatsManager = GetComponent<CharacterStatsManager>(); 
    } 
 
    public virtual void MoveAlongAxis(Vector3 axis)
    {
        axis.x *= _characterStatsManager.MovementStat.X_Axis_Movespeed;
        axis.z *= _characterStatsManager.MovementStat.Y_Axis_Movespeed; 
        _characterController.Move(axis * Time.deltaTime); 
    }

    public void OnDamageEvent(float realDmg)
    {
        _characterStatsManager.HealthStat.ApplyDamage(realDmg);

        DamageEvent?.Invoke(realDmg) ;

    }

    
    public void SetCharacterForward2DDirection(float x)
    {
        if (x > 0)
            _faceingDirection = 1;
        else if (x < 0)
            _faceingDirection =  -1;
    }

    public CharacterController GetCharacterController()
    {
        return _characterController; 
    }
    public int GetFaceingDirection() { return _faceingDirection ; }



}
