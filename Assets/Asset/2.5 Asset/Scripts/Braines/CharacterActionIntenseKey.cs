using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brainintent
{
    public enum ECharacterActionintentKey
    {
        Idle = 0 ,
        MovementX,
        MovementZ, 
        Attack,
        Hurt,
        Interact,
        /*Mostly for enemy behavior */
        Aleart,
          
    };
    public class CharacterActionintent 
    {
        private float _value  ;
        private bool _locked = false ;
 
        ECharacterActionintentKey _key ; 

        #region Methods 
        public CharacterActionintent(ECharacterActionintentKey key)
        {
            this._key = key;
        }

        public bool IsHolding() { return _value != 0; }

        public void ResetIntent()
        {
            _value = 0; 
        }
        public void SetIntent(float value)
        {
            if (_locked)
            {
                Debug.Log("Action " + _key + "cannot be performed");
                return;
            }
            _value = value;
        }
        public float GetValue()
        {
            return _value;
        }
        #endregion
    }
}



 