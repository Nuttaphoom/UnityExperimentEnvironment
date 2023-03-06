using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicCharacterController))]
public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    private List<AttackCollider> _attackColliders ;

    private BasicCharacterController _characterController;
    private List<GameObject> _damagedTarget = new List<GameObject>();

    bool _isAttacking = false;
    float _ttl = 0;
    float _timeSinceStart = 0; 
    private void Update()
    {
        if (_isAttacking)
        {
            _timeSinceStart += 1 * Time.deltaTime;

            if (_timeSinceStart < _ttl)
            {
                ApplyDamageToUndamagedTargets();
            }
            else
                EndAttack();
        }
    }
    public void RegisterAttack(float TTL)
    {
        _ttl = TTL ;
        _isAttacking = true;
        _timeSinceStart = 0; 

        foreach (AttackCollider attackCollider in _attackColliders)
        {
            attackCollider.EnableCollider();
        }
    }

    private void EndAttack()
    {
        _isAttacking = false;
        _timeSinceStart = 0 ;
        _ttl = Mathf.Infinity ;

        _damagedTarget.Clear();
        foreach (AttackCollider attackCollider in _attackColliders)
        {
            attackCollider.DisableCollider();
        }
    }

    public void ApplyDamageToUndamagedTargets()
    {
        for (int i = 0; i < _attackColliders.Count; i++)
        {
            List<GameObject> colObj = _attackColliders[i].GetCollidedObject();

            foreach (GameObject col in colObj)
            {

                if (! _damagedTarget.Contains(col) )
                {
                    col.GetComponent<HurtCollider>().Hurt(1); 
                    _damagedTarget.Add(col); 
                }
            }
        }
    }

 
}
