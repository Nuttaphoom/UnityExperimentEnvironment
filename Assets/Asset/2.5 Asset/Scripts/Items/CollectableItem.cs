using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    [SerializeField]
    private ItemSO _itemSO;
    public ItemSO GetItemSO () { return _itemSO ; }

    //Any Object that have specific callback event will need different class 
    //that inherite from CollectableItem 

    public UnityAction<GameObject> ObjectInteractionEvent ; 

    public void InteractWithObject( GameObject user)
    {
        ObjectInteractionEvent?.Invoke(user); 
    }
}
