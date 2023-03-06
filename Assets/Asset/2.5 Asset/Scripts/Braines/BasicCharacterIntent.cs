using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BasicCharacterintent  
{
    private Dictionary<Brainintent.ECharacterActionintentKey, Brainintent.CharacterActionintent> _intentCache ;
    public delegate void OnActionRequestDelegate(BasicCharacterintent intent);
    public event OnActionRequestDelegate OnActionRequest;

    #region methods 

    public BasicCharacterintent()
    {
        _intentCache = new Dictionary<Brainintent.ECharacterActionintentKey, Brainintent.CharacterActionintent>(); 

        foreach (Brainintent.ECharacterActionintentKey key in System.Enum.GetValues(typeof(Brainintent.ECharacterActionintentKey)))
        {
            Brainintent.CharacterActionintent actionIntent = new Brainintent.CharacterActionintent(key);
            _intentCache.Add(key, actionIntent);
        }
    }

    public float GetIntentActionVlue(Brainintent.ECharacterActionintentKey key)
    {
        return _intentCache[key].GetValue() ;  
    }
    public bool IsHolding(Brainintent.ECharacterActionintentKey key)
    {
        return _intentCache[key].IsHolding() ; 
    }

    public void Setintent(Brainintent.ECharacterActionintentKey _key,float value) 
    {
        if (! _intentCache.ContainsKey(_key))
        {
            return; 
        }
 
        _intentCache[_key].SetIntent(value);
     }

    public void RequestAction()
    {
        OnActionRequest?.Invoke(this);
    }

    public void ResetIntents()
    {
        foreach (Brainintent.ECharacterActionintentKey key in _intentCache.Keys)
        {
            _intentCache[key].ResetIntent();
        }
    }
    #endregion

}

