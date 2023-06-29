using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PongEntity : MonoBehaviour
{
    protected PongStateMachine _state = PongStateMachine.Pausing; 

    [SerializeField]
    private float _speed = 3.0f ;

    private void Update()
    {
        PongTick();
        if (_state == PongStateMachine.Pausing)
        {
            GetComponent<SpriteRenderer>().color = Color.red; 
        }else
        {
            GetComponent<SpriteRenderer>().color = Color.green;  
        }
    }

    abstract protected void PongTick();
    
    protected void Move(Vector2 direction)
    {
        transform.Translate(direction.normalized * _speed * Time.deltaTime );  
    }
}

public enum PongStateMachine
{
    Pausing, 
    Moving 
}

