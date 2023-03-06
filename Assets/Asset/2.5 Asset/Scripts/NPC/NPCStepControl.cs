using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Take care of Conversation with Player, Giving quests, trade object, etc.
//Place along with NPC

[RequireComponent(typeof(NPC))]
public class NPCStepControl : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogueSO ;

    [SerializeField]
    private DialogueLineEventChannelSO _startDialogue = null;
    
    public DialogueSO GetDialogueSO()
    {
        return _dialogueSO ;
    }

    public void InteractionWithProtagonist()
    {
        if (_startDialogue != null )
        {
            _startDialogue.RaiseEvent(_dialogueSO);
        }
    }
}
