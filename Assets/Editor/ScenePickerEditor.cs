using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScenePicker), true)]
public class ScenePickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var picker = target as ScenePicker;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.scenePath);
        var anotherOldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.anotherScenePath);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;
        var secondScene = EditorGUILayout.ObjectField("second scene", anotherOldScene, typeof(SceneAsset), false) as SceneAsset; //

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("scenePath");
            scenePathProperty.stringValue = newPath;

            var secondPath = AssetDatabase.GetAssetPath(secondScene); //
            var secondScenePathProperty = serializedObject.FindProperty("anotherScenePath");
            secondScenePathProperty.stringValue = secondPath;
        }
        serializedObject.ApplyModifiedProperties();
    }
}