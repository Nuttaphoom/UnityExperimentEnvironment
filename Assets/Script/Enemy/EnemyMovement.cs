using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _playerTransform;
    private CharacterController _cc;
    private float _curSpeed;  
    [SerializeField]
    private float _speed; 

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _playerTransform = FindObjectOfType<PlayerController>().transform;
    }


    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        Vector2 downVector = Vector2.down;
        _cc.Move(downVector * 1);
    }

    private void LookAtPlayer()
    {
        Vector3 eTop = _playerTransform.transform.position - transform.position;
        eTop.y = 0;
        transform.rotation = Quaternion.LookRotation(eTop,new Vector3(0,1,0));
    }

    #region GETTER 
    public CharacterController GetCharacterController()
    {
        return _cc;
    }
    public Transform GetPlayerTransform()
    {
        return _playerTransform;
    }
    public GameObject GetPlayerObj()
    {
        return FindObjectOfType<PlayerCombbatController>().gameObject;
    }
    public float GetSpeed()
    {
        return _speed;
    }
    public float GetCurSpeed()
    {
        return _curSpeed; 
    }

    public void SetCurSpeed(float s)
    {
        _curSpeed = s ;
        if (_curSpeed > 1) _curSpeed = 1;
        else if (_curSpeed < -1) _curSpeed = -1;
    }
    #endregion
}
