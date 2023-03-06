using UnityEditor;
using UnityEngine;

public class HurtCollider : MonoBehaviour
{
    [SerializeField]
    private HurtHandler _hurtHandler;

    public void Hurt(float rawDmg)
    {
        _hurtHandler.Hurt(rawDmg);
    }
}