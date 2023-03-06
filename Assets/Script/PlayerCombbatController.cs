using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombbatController : MonoBehaviour
{
    CharacterController _cc;
    Animator _animator;
    Enemy _currentTarget;
    bool forceStopLerping;

    public void Attack(Enemy target,int Hash,float lerpSpeed)
    {
        _currentTarget = target; 
        Vector3 desiredMovePos = Vector3.MoveTowards(target.transform.position, transform.position, 1.0f);
        StartCoroutine(LerpPosition(desiredMovePos, 5,lerpSpeed)) ;

        GetComponent<PlayerController>().AnimatorTriger(Hash);
    }

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>(); 
    }

    public IEnumerator LerpPosition(Vector3 targetPosition, float duration, float lerpSpeed)
    {
        Vector3 lookDirection = targetPosition - transform.position;
        lookDirection.y = 0; 
        float time = 0;
        float distance = (targetPosition - transform.position).magnitude; 
        Vector3 startPosition = transform.position;
        forceStopLerping = false;
        transform.rotation = Quaternion.LookRotation(lookDirection);

        while (! forceStopLerping && time < duration && (transform.position - targetPosition).magnitude > 0.1f)
        {
             transform.rotation = Quaternion.LookRotation(lookDirection);
            _cc.Move((targetPosition - transform.position).normalized   * distance* Time.deltaTime * lerpSpeed); 
            time += Time.deltaTime;
            yield return null;
        }
        forceStopLerping = false; 
        transform.position = targetPosition;
    }
    
    public void StopLerping()
    {
        forceStopLerping = true; 
    }
    void PlayEnemyTargetHitAnimation(string enemyAnimation)
    {
        _currentTarget.Damaged(1,enemyAnimation); 
    }

    public void Damaged(float damage)
    {
        PlayerStateController psc = GetComponent<PlayerController>().GetPlayerStateController() ; 
        psc.SetState(PlayerStateController.EPlayerState.Hit);
        psc.GetFSM().GetCurrentState().SetLifeSpan(1.1f);
    }

    public bool SetEnemyToCounter(out Enemy em)
    {
        Enemy[] enemies = FindObjectOfType<EnemyManager>().GetEnemies();
        em = null;   
        foreach (Enemy e in enemies)
        {
            if (e.GetState() == EnemyStateController.EEnemyState.Attack)
            {
                em = e; 
                return true; 
            }
        }
        return false ;
    }
}

     


 
