#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HazardOrganizer))]
public class HazardOrganizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Repopulate Containers"))
        {
            HazardOrganizer hazardOrganizer = (HazardOrganizer)target;
            hazardOrganizer.PopulateListFromChildren();
        }
        GUI.backgroundColor = Color.white;

        GUILayout.Space(20);

        DrawDefaultInspector();
    }
}

#endif
