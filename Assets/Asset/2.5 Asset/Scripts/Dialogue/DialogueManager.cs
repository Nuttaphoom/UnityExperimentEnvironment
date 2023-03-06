using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private UIDialogueManager _uiDialogueManager; 

    [Header("Listen to")]
    [SerializeField] private DialogueLineEventChannelSO _startDialogue ;

    [Header("Boardcast to")]
    [HideInInspector] public UnityAction<Line> UpdateDialogueEvent;
    [SerializeField] private VoidEventChannelSO _endDialogue;

    bool _isAdvancing = false;

    //Variables for advancing dialogue  
    private DialogueSO _currentDialogueSO;
    private int _currentLineIndex = 0;

    private void OnEnable()
    {
        _startDialogue._raiseEvent += EnterDialogueState;
        _inputReader.DialogueAdvanceEvent += AdvanceDialogue;
        _uiDialogueManager.DoneAdvanceDialogueEvent += DoneAdvancingDialogue;
    }

    private void OnDisable()
    {
        _startDialogue._raiseEvent -= EnterDialogueState;
        _inputReader.DialogueAdvanceEvent -= AdvanceDialogue;
        _uiDialogueManager.DoneAdvanceDialogueEvent -= DoneAdvancingDialogue;
    }



    private void UpdateDialogue(Line line)
    {
        UpdateDialogueEvent?.Invoke(line);
    }

    #region Event_Listener 
    private void EnterDialogueState(DialogueSO dialogueSO)
    {
        _inputReader.SetDialogueActive();
        _currentDialogueSO = dialogueSO;
        AdvanceDialogue();
    }

    private void AdvanceDialogue()
    {
        if (_isAdvancing)
            return;

        if (_currentLineIndex >= _currentDialogueSO.LinesSize)
        {
            EndDialogueManager(); 
            return; 
        }
        _isAdvancing = true;

        UpdateDialogue(_currentDialogueSO.Lines(_currentLineIndex));
    }

    private void DoneAdvancingDialogue()
    {
        _currentLineIndex = (_currentLineIndex + 1);
        _isAdvancing = false;
    }

    private void EndDialogueManager()
    {
        _endDialogue.RaiseEvent(); 

        _currentLineIndex = 0;
        _isAdvancing = false;

        _inputReader.SetGameplayActive(); 
    }

    #endregion  


}
