using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(FieldOfViewDetector))]
public class CustomToolFieldofViewVisualizer : Editor
{
    FieldOfViewDetector _target => target as FieldOfViewDetector; 
    public void OnSceneGUI()
    {
        var tr = _target.transform;
        var pos = tr.position;
        // display an orange disc where the object is
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireArc(pos, tr.up, tr.forward, 360, _target.GetRadius(), 3);
        Handles.color = Color.red ;

        int faceDirection = _target.GetFaceDirection();
        
        Vector3 viewAngleA = new Vector3(Mathf.Cos(_target.GetAngleInRad() / 2 ) , 0,  Mathf.Sin(_target.GetAngleInRad() / 2    )   );
        Vector3 viewAngleB = new Vector3(Mathf.Cos(_target.GetAngleInRad() / 2 )  , 0, -1 * Mathf.Sin(_target.GetAngleInRad() / 2 ));

        viewAngleA *= faceDirection;
        viewAngleB *= faceDirection; 

        Handles.DrawLine(pos,  pos +    viewAngleA    * _target.GetRadius(), 3);
        Handles.DrawLine(pos,  pos +   viewAngleB    * _target.GetRadius(), 3);
    }
}
