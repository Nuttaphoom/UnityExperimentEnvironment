using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class AIPongController : PongEntity
{
    protected override void PongTick()
    {
        PongBall ball = FindObjectOfType<PongBall>();
        Vector3 dir = (ball.transform.position - transform.position).normalized;
        dir.x = 0;



        if (dir.magnitude == 0)
        {
            _state = PongStateMachine.Pausing;
        }
        else
        {
            Move(dir);

            _state = PongStateMachine.Moving;
        }

    }

   
}
