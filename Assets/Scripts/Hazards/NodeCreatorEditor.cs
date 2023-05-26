#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeCreator))]
public class NodeCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Create Node"))
        {
            NodeCreator nodeCreator = (NodeCreator)target;
            nodeCreator.AddNode();
        }
    }
}

#endif