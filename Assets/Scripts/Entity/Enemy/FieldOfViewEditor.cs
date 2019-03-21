using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/* Defines custom editor behavior for the field of view class.
 * As the value for the field of view radius changes, the editor
 * will draw a circle centered on the player to reflect it.
 */

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        // Draw a circle of the given view radius centered on the player
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);

        // Draw the left line delineating the left edge of the FOV cone
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);

        // Draw the right line delineating the right edge of the FOV cone
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        // Draw a red line to the visible targets
        if(fov.player != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.player.position);
        }
    }
}
