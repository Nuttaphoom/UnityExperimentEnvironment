using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private EnemyManager _enemyManager ;

   

    public bool GetDetectedEnemy(out Enemy hitEnim )
    {
        RaycastHit hit;
        hitEnim = null;  
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;

        if (Physics.SphereCast(transform.position, 2.0f, camForward, out hit))
            if (hit.collider.GetComponent<Enemy>())
            {
                Debug.DrawLine(transform.position, hit.collider.transform.position, Color.red);

                hitEnim = hit.collider.GetComponent<Enemy>();
                return true;
            }


        return false ; 
    }
}
