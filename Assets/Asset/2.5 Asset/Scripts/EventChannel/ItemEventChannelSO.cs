using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemEventChannelSO", menuName = "ScriptableObject/EventChannel/ItemEventChannelSO")]
public class ItemEventChannelSO : DescriptionBaseSO
{
    public UnityAction<GameObject,ItemSO> _raiseEvent; 

    public void RaiseEvent(GameObject _picker, ItemSO so)
    {
        _raiseEvent?.Invoke(_picker, so); 
    }

}
