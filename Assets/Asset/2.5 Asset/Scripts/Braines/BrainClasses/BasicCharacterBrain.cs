using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicCharacterController))]
public abstract class BasicCharacterBrain : MonoBehaviour
{
    protected BasicCharacterController _basicCharacterController; 
    protected BasicCharacterintent _characterintent;
    private Dictionary<Brainintent.ECharacterActionintentKey, float> _tempIntent;

    protected void Awake()
    {
        _basicCharacterController = GetComponent<BasicCharacterController>();
    }
    protected void OnEnable()
    {
        _basicCharacterController.DamageEvent += OnDamageCharacter;
    }

    protected void OnDisable()
    {
        _basicCharacterController.DamageEvent -= OnDamageCharacter;
    }

    protected void Update()
    {
        this.ManipulateBrain()  ;
        this.CombineOutsideAndInsideIntent(); 
        this.RequestAction() ;
        this.ResetIntents(); 
    }

    public void CombineOutsideAndInsideIntent()
    {
        if (_tempIntent == null)
            return; 

        foreach (Brainintent.ECharacterActionintentKey key in _tempIntent.Keys)
        {
            _characterintent.Setintent(key, _tempIntent[key]);
        }
        _tempIntent.Clear(); 
    }
    public virtual BasicCharacterintent GetCharacterIntents()
    {
        if (_characterintent == null)
            _characterintent = new BasicCharacterintent(); 
        return _characterintent;
    }
    protected void ResetIntents()
    {
        GetCharacterIntents().ResetIntents(); 
    }

 
    protected void RequestAction()
    {
        GetCharacterIntents().RequestAction();
    }

    protected void Setintent(Brainintent.ECharacterActionintentKey _key, float value)  
    {  
        GetCharacterIntents().Setintent(_key, value); 
    }

    public void SetintentFromOutsideSource(Brainintent.ECharacterActionintentKey _key)
    {
        if (_tempIntent == null)
            _tempIntent = new Dictionary<Brainintent.ECharacterActionintentKey, float>(); 
        _tempIntent[_key] = 1;
    }


    #region EventListen
    public void OnDamageCharacter(float dmg)
    {
        SetintentFromOutsideSource(Brainintent.ECharacterActionintentKey.Hurt); 
    }
    #endregion


    #region Abstract
    abstract protected void ManipulateBrain();

   
    #endregion
}
