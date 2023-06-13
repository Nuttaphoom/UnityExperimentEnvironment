using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class AIPongController : PongEntity
{
    void Update()
    {
        PongBall ball = FindObjectOfType<PongBall>();
        Vector3 dir = (ball.transform.position - transform.position).normalized;
        dir.x = 0;
        Move(dir);  
         
    }
}
