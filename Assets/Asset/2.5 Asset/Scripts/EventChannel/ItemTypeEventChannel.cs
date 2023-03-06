using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
 

[CreateAssetMenu(fileName = "ItemTypeEventChannel", menuName = "ScriptableObject/EventChannel/ItemTypeEventChannel")]
public class ItemTypeEventChannel : DescriptionBaseSO
{
    public UnityAction<ItemInventoryType> _raiseEvent;

    public void RaiseEvent(ItemInventoryType type)
    {
        _raiseEvent?.Invoke(type);
    }

}