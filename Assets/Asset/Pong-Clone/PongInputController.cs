using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongInputController : PongEntity
{
   
    void Update()
    {
        float ver = Input.GetAxis("Vertical");
        Move(new Vector2(0,ver)) ;
    }
}
