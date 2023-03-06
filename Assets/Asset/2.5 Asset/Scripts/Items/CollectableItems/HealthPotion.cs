using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public  class HealthPotion : CollectableItem
{
    private void OnEnable()
    {
        ObjectInteractionEvent += HealUser;
    }

    private void OnDisable()
    {
        ObjectInteractionEvent -= HealUser; 
    }

    public void HealUser(GameObject user)
    {

    }

}
