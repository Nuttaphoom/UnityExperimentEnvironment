using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events; 

public class UIDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speakerNameText ;
    [SerializeField] private TextMeshProUGUI _lineText;
    [SerializeField] public DialogueManager _dialogueManager;
    [SerializeField] private GameObject _renderer; 

    [Header("Listen to")]
    [SerializeField] private DialogueLineEventChannelSO _startDialogue ;
    [SerializeField] private VoidEventChannelSO _endDialogue;

    [Header("Boardcast to")]
    [HideInInspector] public UnityAction DoneAdvanceDialogueEvent;

    private void OnEnable()
    {
        _dialogueManager.UpdateDialogueEvent += UpdateDialogue ;
        _startDialogue._raiseEvent += StartDialogue;
        _endDialogue._raiseEvent += CloseDialogue; 
    }

    private void OnDisable()
    {
        _dialogueManager.UpdateDialogueEvent -= UpdateDialogue;
        _startDialogue._raiseEvent -= StartDialogue;
        _endDialogue._raiseEvent -= CloseDialogue;

    }

    private IEnumerator UpdateDialogueCoruntine(Line line)
    {
        _lineText.text = ""; 
        for (int i = 0; i < line.TextLine.Length; i++)
        {
            _lineText.text += line.TextLine[i];

            yield return new WaitForSeconds(line.AdvanceSpeed); 
        }
        
        DoneAdvanceDialogueEvent?.Invoke(); 
    }

    //Event Listener 
    private void StartDialogue(DialogueSO so  )
    {
        _renderer.SetActive(true) ; 
    }

    private void UpdateDialogue(Line line)
    {
        _speakerNameText.text = line.SpeakerName;
        StartCoroutine(UpdateDialogueCoruntine(line)); 
    }

    private void CloseDialogue()
    {
        _renderer.SetActive(false);

    }


}
