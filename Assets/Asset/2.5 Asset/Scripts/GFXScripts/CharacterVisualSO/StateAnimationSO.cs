using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "StateAnimationSO", menuName = "ScriptableObject/AnimationSO/StateAnimationSO")]

public class StateAnimationSO : ScriptableObject
{
    public string AnimationName;
    [Header("Duration in Animator. Will be snap from 0.1 - 0.6 to 0.1 - 1.0 in SO")]
    [SerializeField]private float _durationInSecond;

    public float DurationInSecond => _durationInSecond * 100 / 60;
}