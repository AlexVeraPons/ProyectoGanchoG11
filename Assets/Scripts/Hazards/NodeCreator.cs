using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeCreator : MonoBehaviour
{
    private List<Vector2> _nodes = new List<Vector2>();
    [SerializeField]
    private GameObject _nodePrefab;
    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _nodes[i] = this.transform.GetChild(i).position;
        }
    }

    public void AddNode()
    {
        GameObject node = Instantiate(_nodePrefab, this.transform);
        _nodes.Add(node.transform.position);
    }

}

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
