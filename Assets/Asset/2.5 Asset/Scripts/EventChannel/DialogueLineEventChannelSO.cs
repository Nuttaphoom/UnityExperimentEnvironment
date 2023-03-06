using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueLineEventChannelSO", menuName = "ScriptableObject/EventChannel/DialogueLineEventChannelSO")]
public class DialogueLineEventChannelSO : DescriptionBaseSO
{
    public UnityAction<DialogueSO> _raiseEvent;

    public void RaiseEvent(DialogueSO d)
    {
        _raiseEvent?.Invoke(d) ;
    }

}
