using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(fileName = "SceneEventChannel", menuName = "ScriptableObject/EventChannel/SceneEventChannel")]

public class SceneEventChannel : DescriptionBaseSO
{
    public UnityAction<SceneSO> _raiseEvent;

    public void RaiseEvent(SceneSO scene)
    {
        _raiseEvent?.Invoke(scene);
    }
}
 