using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(fileName = "VoidEventChannelSO", menuName = "ScriptableObject/EventChannel/VoidEventChannelSO")]
public class VoidEventChannelSO : DescriptionBaseSO
{
    public UnityAction _raiseEvent;

    public void RaiseEvent()
    {
        _raiseEvent?.Invoke() ;
    }

}
