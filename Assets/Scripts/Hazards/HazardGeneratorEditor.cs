#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HazardGenerator))]
public class HazardGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HazardGenerator hazardGenerator = (HazardGenerator)target;

        GUILayout.Space(20);

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Regenerate Hazard List"))
        {
            hazardGenerator.RegenereateGameObjects();
        }
        GUI.backgroundColor = Color.white;

        GUILayout.Space(30);

        GUILayout.Label("Hazard List", EditorStyles.boldLabel);

        foreach (string hazardName in hazardGenerator._hazardNames)
        {
            if (GUILayout.Button(hazardName))
            {
                hazardGenerator.AddHazardFromName(hazardName);
            }
        }

        GUILayout.Space(30);

        base.DrawDefaultInspector();

        GUILayout.Space(20);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Generate Hazards in new Container"))
        {
            hazardGenerator.GenerateHazardsInNewContainer();
        }
        GUI.backgroundColor = Color.white;
    }
}

#endif
