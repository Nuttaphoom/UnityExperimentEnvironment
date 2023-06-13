using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEntity : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f ; 
    protected void Move(Vector2 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime );  

    }
}
