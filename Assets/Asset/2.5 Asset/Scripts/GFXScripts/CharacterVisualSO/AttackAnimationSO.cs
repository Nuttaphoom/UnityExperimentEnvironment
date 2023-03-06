using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAnimationSO" , menuName = "ScriptableObject/AnimationSO/AttackAnimationSO")]
public class AttackAnimationSO : StateAnimationSO
{
    [Header("Time Interval is check with animation time (basically 1 - 60) ")]
    [Header("Interval For Combo Attack. In Second")]
    [SerializeField]
    private float ComboStartFrame;
    [SerializeField]
    private float ComboEndFrame;

    [Header("Interval For Dealing Damage. In Second")]
    [SerializeField]
    private float DealDMGStartFrame;
    [SerializeField]
    private float DealDMGEndFrame;

    

    public bool IsInsideInterval(int type, float t)
    {
        float start = 1;
        float end = -1;


        if (type == 0)//Combo
        {
            start = ComboStartFrame;
            end = ComboEndFrame;
        }else if (type == 1) //DealDMG
        {
            start = DealDMGStartFrame;
            end = DealDMGEndFrame; 
        }

        //Snap between animation frame and real time 
        start =   ( start * 100 / 60 )  ;
        end =   ( end * 100 / 60 )   ;
        float m = Mathf.InverseLerp(start, end, t);
        return (m > 0 && m < 1) ;
         
    }

 



    public float GetIntervalDuration(int type)
    {
        float start = 1;
        float end = -1;
        if (type == 0)//Combo
        {
            start = ComboStartFrame;
            end = ComboEndFrame;
        }
        else if (type == 1) //DealDMG
        {
            start = DealDMGStartFrame;
            end = DealDMGEndFrame;
        }

        //Snap between animation frame and real time 
        start = start * 100 / 60;
        end = end * 100 / 60;

        return end - start;
    }

}