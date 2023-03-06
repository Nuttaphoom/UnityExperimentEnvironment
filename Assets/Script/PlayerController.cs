using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private CharacterController _cc;
    private PlayerDetection _playerDetection;
    private PlayerCombbatController _playerComatController;
    private PlayerStateController _playerStateController; 

    [SerializeField]
    private float _speed = 0.1f;
    [SerializeField]
    private float _maxSpeed = 0.5f;
 
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerDetection = GetComponent<PlayerDetection>();
        _playerComatController = GetComponent<PlayerCombbatController>();
        _playerStateController = new PlayerStateController(gameObject);
        _cc = GetComponent<CharacterController>(); 
     }

    // Update is called once per frame
    void Update()
    {
        _playerStateController.StateUpdate();
       ApplyGravity();
     }

    private void ApplyGravity()
    {
        Vector2 downVector = Vector2.down ;
        _cc.Move(downVector * 1);
    }

    public void AnimatorTriger( int animationHash )
    {
        GetAnimator().SetTrigger(animationHash);
    }

    public void AnimatorSetInt(int animationHash,int  numbers)
    {
        GetAnimator().SetInteger(animationHash, numbers); 
    }

    public void AnimatorSetFloat(int animationHash, float numbers)
    {
        GetAnimator().SetFloat(animationHash, numbers);
    }

    /*private void MouseHandler()
    {
        
    }*/

    #region Getter 
    public Animator GetAnimator()
    {
        return _animator; 
    }

    public CharacterController GetCharacterController()
    {
        return _cc;
    }

    public PlayerStateController GetPlayerStateController()
    {
        return _playerStateController; 
    }

    public PlayerCombbatController GetCombatController()
    {
        return _playerComatController; 
    }

    public PlayerDetection GetPlayerDetection()
    {
        return _playerDetection; 
    }

    public float GetSpeed()
    {
        return _speed;
    }

    /*public (float, float) GetVelocity()
    {
        return (_velocityX, _velocityZ);
    }*/

    #endregion

    /*private void Movement()
    {
        Vector3 forward = Camera.main.gameObject.transform.forward;
        Vector3 right = Camera.main.gameObject.transform.right;

        forward.y = 0;
        right.y = 0;

        Vector3 movingDirection = forward * _velocityZ + right * _velocityX ;

        _velocityX = Input.GetAxis("Horizontal");
        _velocityZ = Input.GetAxis("Vertical"); 

        if (Mathf.Abs(_velocityZ) > 0 || Mathf.Abs(_velocityX) > 0 ) 
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movingDirection), 0.2f) ;

        _cc.Move(movingDirection * Time.deltaTime * _speed);
    }*/
}
