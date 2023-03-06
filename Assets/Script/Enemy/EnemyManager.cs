using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Enemy[] enemies ;
    private float randomAttackTimer = -1 ;
    private float timer = 0; 

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();    
    }

    private void Update()
    {
        randomAttackTimer = Random.Range(3, 6) ;  
        if (timer > randomAttackTimer)
        {
            timer = 0;
            int i   ;
            int attemp = 0;
            do
            {
                attemp += 1; 
                i = Random.Range(0, enemies.Length); ;

            } while ((enemies[i].GetState() != EnemyStateController.EEnemyState.Idle) && attemp < 100);

            enemies[i].ChangeFSMState(EnemyStateController.EEnemyState.Attack); 
        }
        timer += 1 * Time.deltaTime; 
    }

        
    public Enemy[] GetEnemies()
    {
        return enemies; 
    }
}
