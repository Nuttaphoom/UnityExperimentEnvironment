using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterAnimationSO", menuName = "ScriptableObject/CharacterAnimationSO")]
public class CharacterAnimationSO : ScriptableObject
{ 
    [SerializeField]
    private List<Brainintent.ECharacterActionintentKey> _keys;
    [SerializeField]
    private List<string> _animationName; 

    public string GetAnimationName(Brainintent.ECharacterActionintentKey key)
    {
       return  _animationName[ _keys.IndexOf(key)] ; 
    }
}
