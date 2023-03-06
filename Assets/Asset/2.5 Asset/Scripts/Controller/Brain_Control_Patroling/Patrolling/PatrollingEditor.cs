using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PatrolLogic))]
public class PatrollingEditor : MonoBehaviour
{
    protected List<Vector3> points = new List<Vector3>() ;

    private void Update()
    {
        foreach (Vector3 v in points)
        {
            Debug.Log(v); 
        }
    }
    public void AddPoint(Vector3 point)
    {
        points.Add(point);
    }

    public void InsertPoint(Vector3 point, int index)
    {
        points.Insert(index,point) ; 
    }

    public void RemovePoint(int index)
    {
        points.RemoveAt(index); 
    }

    public void PopPoint()
    {
        points.RemoveAt(points.Count - 1); 
    }

    public void SetPatrolPoints(Vector3[] vecs, int size)  
    {
        points.Clear(); 
        for (int i =0; i < size; i++)
        {
            Debug.Log(vecs[i]);
            AddPoint(vecs[i]);
        }
    }

    public (int, Vector3[]) GetPatrolPointDetail()
    {
        Vector3[] p;  
        int size ;
        (p, size) = GetComponent<PatrolLogic>().GetBakedPoints();


        return (size, p);
    }

    public void BakePoints()
    {
        GetComponent<PatrolLogic>().BakePoints(points); 
    }

    
}