using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor  ;


[CustomEditor(typeof(BaseCharacterFSM))]
public class CustomToolStateExamine : Editor 
{
    bool _isEnable = false ;
    BaseCharacterFSM _fsm = null; 
    private void OnEnable()
    {
        _isEnable = false; 
        _fsm = target as BaseCharacterFSM;
    }
    void OnSceneGUI()
    {
        if (!_isEnable)
            return; 
         
          
        GUIContent content = new GUIContent(_fsm.GetCurrentStateType().ToString() ); 
        Handles.Label(_fsm.transform.position, content); 
    }

    
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Toggle State Examine"))
        {
            _isEnable = !_isEnable; 
        }

    }
}
