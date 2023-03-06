using UnityEditor;
using UnityEngine;

 public class CharacterAnimatorHandler : MonoBehaviour
{
    [Header("===== Need to be set =====")]
    [Header("Animator in GFX Obj")]
    [SerializeField] 
    private Animator _animator;

    [Header("Scriptable Object containing animation name associated with action key")]
    [SerializeField]
    private CharacterAnimationSO _characterAnimationSO;

    private string _currentAnimation ;

    public void SetBool(string s, bool value)
    {
        _animator.SetBool(s, value);  
    }
    public void SetFloat(string s, float value)
    {
        _animator.SetFloat(s, value);
    }
    public void PlayAnimation(string animationName)
    {
        if (! _animator.HasState(0,Animator.StringToHash(animationName)))
        {
            throw new System.Exception("No Necessary Animation For " + gameObject.name);
        }

        if (_currentAnimation == animationName)
        {
            return;
        }


        _animator.Play(animationName);

        _currentAnimation = animationName;

    }

    public void PlayAnimation(Brainintent.ECharacterActionintentKey actionKey)
    {
        PlayAnimation(_characterAnimationSO.GetAnimationName(actionKey) );
    }

   
    public void SetVisualForward2DDirection(float x)
    {
        Vector3 tmp = _animator.gameObject.transform.localScale;
        if (x > 0)
            tmp.x = Mathf.Abs(tmp.x);
        else if (x < 0)
            tmp.x = Mathf.Abs(tmp.x) * -1; 

        _animator.gameObject.transform.localScale = tmp ;  
    }

    public (float,bool) GetRealTimeSinceAnimationStart()
    {
        Debug.Log("anim time : " + _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        return  ( _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * _animator.GetCurrentAnimatorStateInfo(0).normalizedTime , _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }
    
}