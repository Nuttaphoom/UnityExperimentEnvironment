using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterNavAgent : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent _agent;

    [SerializeField]
    private GameObject _characterAgent;

    [SerializeField]
    private float _GroundY; 

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = 0; 
    }

    private void Update()
    {
        _agent.SetDestination(Vector3.zero);
    }

    public void SetDestination(Vector3 pos)
    {
        _agent.SetDestination(pos); 
    }

    public (Vector3[],bool) GetCornerPath()
    {
        NavMeshPath _path = _agent.path;

        Vector3[] ret = new Vector3[_path.corners.Length] ;
        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = _path.corners[i];
        }

        return (ret, _agent.hasPath) ;
    }


}
