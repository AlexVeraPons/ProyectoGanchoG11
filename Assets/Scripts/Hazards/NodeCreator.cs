using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeCreator : MonoBehaviour
{
    private List<Vector2> _nodes = new List<Vector2>();

    [SerializeField]
    private GameObject _nodePrefab;

    [SerializeField]
    private bool _gizmos = true;
    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _nodes.Add(this.transform.GetChild(i).position);
        }
    }

    public void AddNode()
    {
        GameObject node = Instantiate(_nodePrefab, this.transform);
        node.name = "Node " + this.transform.childCount;

        _nodes.Add(node.transform.position);
    }

    private void OnDrawGizmos()
    {
        if (_gizmos == false)
            return;

        // draw lines between nodes

        Gizmos.color = Color.green;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (i != this.transform.childCount - 1)
            {
                Gizmos.DrawLine(
                    this.transform.GetChild(i).position,
                    this.transform.GetChild(i + 1).position
                );
            }
        }
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
