using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongInputController : PongEntity
{
   
     
    
       
    protected override void PongTick()
    {
        float ver = Input.GetAxis("Vertical");
        if (ver != 0)
        {
            Move(new Vector2(0, ver));
            _state = PongStateMachine.Moving; 
        }else
        {
            _state = PongStateMachine.Pausing;  
        }
    }
 
}
