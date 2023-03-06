using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BasicCharacterController))]


public class HurtHandler : MonoBehaviour
{

    private int invisibilityDuration ;
    private BasicCharacterController _basicCharacterController; 

    private void Awake()
    {
        _basicCharacterController = GetComponent<BasicCharacterController>(); 
    }
    public void Hurt(float rawDmg)
    {
        if (invisibilityDuration > 0)
            return;

        float realDmg = CalculateDMG(rawDmg); 
        GetComponent<BasicCharacterController>().OnDamageEvent(realDmg); 

    }

    public float CalculateDMG(float rawDmg)
    { 
        float ret = rawDmg ;
        //Apply any sheild or /and additional herebonus here

        return ret;
    }
}