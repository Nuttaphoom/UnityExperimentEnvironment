using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerCharacterBrain : BasicCharacterBrain
{
    private Vector2 _inputDirection = new Vector2() ; 
    [SerializeField]
    private InputReader _inputReader;

    protected void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        base.OnEnable(); 
        _inputReader.AttackEvent += AttackEvent;
        _inputReader.MoveEvent += MoveEvent;
        _inputReader.InteractEvent += InteractEvent;
    }

    private void OnDisable()
    {
        base.OnDisable();

        _inputReader.AttackEvent -= AttackEvent;
        _inputReader.MoveEvent -= MoveEvent;
        _inputReader.InteractEvent -= InteractEvent;
    }

    protected override void ManipulateBrain()
    {
        Setintent(Brainintent.ECharacterActionintentKey.MovementX, _inputDirection.x);
        Setintent(Brainintent.ECharacterActionintentKey.MovementZ, _inputDirection.y);
    }

    //Event Binder// 

    private void AttackEvent()
    {
        Setintent(Brainintent.ECharacterActionintentKey.Attack, (Input.GetMouseButtonDown(0) == true ? 1 : 0));
    }

    private void MoveEvent(Vector2 vec2)
    {
        _inputDirection.x = vec2.x;
        _inputDirection.y = vec2.y;
    }

    private void InteractEvent()
    {
        Setintent(Brainintent.ECharacterActionintentKey.Interact, 1);
    }
  



}
