using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrollingEditor))]
public class PatrolLogic : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> points;

    public void BakePoints(List<Vector3> p)
    {
        points.Clear();
        for (int i = 0; i < p.Count; i++)
            points.Add(p[i]);
    }

    public (Vector3[],int) GetBakedPoints()
    {
        Vector3[] ret = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            ret[i] = points[i]; 
        }
        return (ret,points.Count) ; 
    }

    public List<Vector3> GetPatrolPoints()
    {
        return points; 
    }
}
