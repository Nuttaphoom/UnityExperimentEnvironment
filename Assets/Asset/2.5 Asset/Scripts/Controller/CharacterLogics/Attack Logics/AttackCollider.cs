using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    List<GameObject> _collidedsObjs = new List<GameObject>();


 

    public List<GameObject> GetCollidedObject()
    {
        return _collidedsObjs; 
    }

    public void DisableCollider()
    {
        _collidedsObjs.Clear();
        GetComponent<Collider>().enabled = false; 
    }

    public void EnableCollider()
    {
        _collidedsObjs.Clear();
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        HurtCollider hurtCollider; 

        if (collision.TryGetComponent(out hurtCollider) ) {
            if (_collidedsObjs.Contains(collision.gameObject))
            {
                return;
            }
            else
            {
                _collidedsObjs.Add(collision.gameObject);
            } 
        }
    }
     
 
}
