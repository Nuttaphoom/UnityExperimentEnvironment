using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrollingEditor))]
public class CustomEnemyPatrolingPath : Editor 
{
    protected bool _isEditing = false               ;
    protected Vector3[] _points = new Vector3[50]   ;
    protected int selectedPoint = -1                ;
     
    PatrollingEditor _target => target as PatrollingEditor ; 
    private int _pointSize = 0; 

    private void OnSceneGUI()
    {
        if (!_isEditing)
            return;

        EventType curEventType = Event.current.type ; 
        if (curEventType == EventType.Repaint)
        {
            DrawSphere(); 
        }else if (curEventType == EventType.MouseDown)
        {
            OnMouseDown(Event.current);
        }else if (curEventType == EventType.MouseDrag)
        {
            OnMouseDrag(Event.current); 
        }else if (curEventType == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(
            GUIUtility.GetControlID(FocusType.Passive));
        }

    }

    public override void OnInspectorGUI()
    {
        if (!_isEditing)
        {
            if (GUILayout.Button("Toggle Editor Mode"))
            {
                _isEditing = !_isEditing;
                Vector3[] p; 
                (_pointSize,p)  = _target.GetPatrolPointDetail(); 
                for (int i = 0; i < _pointSize; i++)
                {
                    _points[i] = p[i]; 
                }
            }
            return;
        }

        if (GUILayout.Button("Add Patrol Point"))
        {   
            _points[_pointSize] = _target.transform.position ;
            _pointSize++; 
        }

        if (GUILayout.Button("Remove Patrol Point"))
        {
            _pointSize--;
            if (_pointSize < 0)
                _pointSize = 0;
        }

        if (GUILayout.Button("Bake"))
        {
            _target.SetPatrolPoints(_points, _pointSize);
            _target.BakePoints();
        }
    }

    #region Methods 
    private void DrawSphere()
    {
        for (int i = 0; i < _pointSize; i++)
        {
            Handles.Label(new Vector3(_points[i].x, _points[i].y + 1  , _points[i].z), "" + i);
            if (i == 0) Handles.color = Color.green;
            else if (i == _pointSize - 1) Handles.color = Color.red;
            else Handles.color = Color.white; 

            Handles.SphereHandleCap(0, _points[i], Quaternion.identity, 1, EventType.Repaint);
        }

        for (int i =0; i < _pointSize - 1; i++)
        {
            Handles.color = Color.white;
            Handles.DrawLine(_points[i], _points[(i + 1) ]); 
        }
    }

    private void OnMouseDown(Event evet)
    {
        if (!_isEditing)
            return;

        selectedPoint = GetSelectedPoint(evet); 
 
    }

    private void OnMouseDrag(Event evet)
    {
        if (!_isEditing)
            return;
        bool isHit; RaycastHit hit; 
        (isHit,hit) = GetMouseHitPoint(evet) ;
        if (!isHit)
            return;

       if (selectedPoint>= 0) _points[selectedPoint] = hit.point; 
    }

    private int GetSelectedPoint(Event evet)
    {
        int index = -1;
        RaycastHit hit; bool isHit;
        (isHit,hit)= GetMouseHitPoint(evet);

        if (!isHit)
            return -1;

        float closest = Mathf.Infinity; 
        for (int i = 0; i < _pointSize; i ++ )
        {
            float dist = Vector3.Distance( _points[i], hit.point); 
            if (dist < HandleUtility.GetHandleSize(_points[i]) / 10 && dist < closest)
            {
                closest = dist;
                index = i; 
            }
        }

        return index; 
        
    }

    private (bool,RaycastHit) GetMouseHitPoint(Event evet)
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(evet.mousePosition);

        if ( (HandleUtility.RaySnap(mouseRay) is RaycastHit hit))
        {
            return (true,hit );
        }
        return (false,new RaycastHit()); 
    } 
    #endregion
}