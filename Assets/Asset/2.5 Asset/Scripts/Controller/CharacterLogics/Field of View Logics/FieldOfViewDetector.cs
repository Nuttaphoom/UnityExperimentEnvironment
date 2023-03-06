using UnityEditor;
using UnityEngine;

public class FieldOfViewDetector : MonoBehaviour
{
    [Header("These Parameter Help in decide whether target is detected")]
    [SerializeField]
    [Header("In Degree")]
    private float _angle ;
    [SerializeField]
    private float _redius;
    
    [SerializeField]
    private string _targetTag;
    private int _faceDirection;
    private GameObject _target ;

  
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(_targetTag); 
    }

    public bool IsTargetDetected(int faceDirection )
    {
        _faceDirection = faceDirection ; 
        Vector3 dir = (transform.position - _target.transform.position).normalized ;
        Collider[] hits = Physics.OverlapSphere(transform.position, GetRadius() );
        for (int i = 0;  i < hits.Length; i ++)
        {
            if (hits[i].gameObject == _target)
            {
                Vector3 vec = (hits[i].gameObject.transform.position - gameObject.transform.position);
                if (Vector3.Angle(vec,transform.right * _faceDirection) <= GetAngleInDeg() / 2)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public float GetRadius() { return _redius;  }
    public float GetAngleInDeg() { return _angle; }
    public float GetAngleInRad() { return _angle * Mathf.Deg2Rad; }

    public int GetFaceDirection() { return _faceDirection; }
    
}