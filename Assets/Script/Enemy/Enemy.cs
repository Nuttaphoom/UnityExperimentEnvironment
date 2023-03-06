using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyStateController enemyStateController  ; 
    protected Animator _animator;

    [Header("Polish")]
    [SerializeField]
    private ParticleSystem counterParticle; 
    
    // Start is called before the first frame update
    void Start()
    {
        _animator =  GetComponent<Animator>()      ;
        enemyStateController =  new EnemyStateController(gameObject)      ;
        TurnParticle(false); 
     }

    private void Update()
    {
        enemyStateController.StateUpdate();
    }
    public void ChangeFSMState(EnemyStateController.EEnemyState s)
    {

        enemyStateController.SetState(s);
    }

    public void AnimatorTriger(int animationHash)
    {
        GetAnimator().SetTrigger(animationHash);
    }

    public void AnimatorSetInt(int animationHash, int numbers)
    {
        GetAnimator().SetInteger(animationHash, numbers);
    }

    public void AnimatorSetFloat(int animationHash, float numbers)
    {
        GetAnimator().SetFloat(animationHash, numbers);
    }

    #region GETTER 
    public EnemyStateController.EEnemyState GetState()
    {
        return GetStateController().GetCurrentEnemyState(); 
    }
    public Animator GetAnimator()
    {
        return GetComponent<Animator>() ; 
    }

    public EnemyStateController GetStateController()
    {
        return enemyStateController; 
    }
    #endregion

    public void Attack()
    {
        GameObject target = FindObjectOfType<PlayerController>().gameObject;
        target.GetComponent<PlayerCombbatController>().Damaged(1);
     }
 

    public void Damaged(int damageAmout,string animationType)
    {
        GetAnimator().SetTrigger(animationType); 
       ChangeFSMState(EnemyStateController.EEnemyState.Hit); 
    }

    public void TurnParticle(bool on)
    {
        if (on)
            counterParticle.Play();
        else
        {
            counterParticle.Clear(); 
            counterParticle.Stop();
        }
    }



}
