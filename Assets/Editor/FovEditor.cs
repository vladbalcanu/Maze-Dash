using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyBehavior))]
public class FovEditor : Editor
{
    // Start is called before the first frame update
    private void OnSceneGUI()
    {
        EnemyBehavior fov = (EnemyBehavior)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.fovRadius);

        Vector3 viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fovHorizontalAngle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fovHorizontalAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.fovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.fovRadius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
