using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[CreateAssetMenu(fileName = "DialogueSO" , menuName = "ScriptableObject/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField]
    private List<Line> _lines;

    public Line Lines(int index) => _lines[index] ;
    public int LinesSize => _lines.Count; 
}

[Serializable]
public class Line
{
    [SerializeField] private string _textLine   ;
    [SerializeField] private string _speaker    ;
    [SerializeField] private Sprite _faceSprite ;
    [SerializeField] private float _advanceSpeed ; 

    public string TextLine => _textLine;
    public string SpeakerName => _speaker;
    public Sprite FaceSprite => _faceSprite;
    public float AdvanceSpeed => _advanceSpeed;
   


}
