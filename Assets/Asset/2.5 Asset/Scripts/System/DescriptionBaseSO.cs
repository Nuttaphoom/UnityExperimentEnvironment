using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBaseSO : ScriptableObject
{ 
    [Header("What does this SO do ? ")]
    [TextArea]
    [SerializeField]
    private string description;  
}
