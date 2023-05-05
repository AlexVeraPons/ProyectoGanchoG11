using System;
using UnityEngine;

public class HazardWithPath : Hazard
{
    [SerializeField]
    private float _linearSpeed = 0;

    [SerializeField]
    [Space(10)]
    [Tooltip(
        "Loop: the chainsaw will go through all the nodes and then start again from the first one.\n \nReturn: the chainsaw will go through all the nodes and then go back to the first one."
    )]
    private RouteType _routeType;

    public enum RouteType
    {
        Loop,
        Return
    }

    [Space(10)]
    [Header("References (DO NOT MODIFY)")]
    [SerializeField]
    [Tooltip("The parent of the nodes that the hazard will go through.")]
    private Transform _nodesParent;

    // the distance between the chainsaw and the node at which the chainsaw will start moving towards the next node
    private const float _distanceThreshold = 0.15f;
    private Rigidbody2D _rigidbody2D;
    private Vector2[] _nodes;
    private int _currentNodeIndex;
    private Vector2 _currentNode => _nodes[_currentNodeIndex];
    private bool _goingBack = false;

    private void Awake()
    {
        _nodes = new Vector2[_nodesParent.childCount];
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private protected override void Appear()
    {
        base.Appear();
        // populate the nodes array
        _nodes[0] = this.transform.position;

        for (int i = 0; i < _nodesParent.childCount; i++)
        {
            _nodes[i] = _nodesParent.GetChild(i).gameObject.transform.position;
        }

        transform.position = _nodes[0];
    }

    private protected override void Disappear()
    {
        this.gameObject.SetActive(false);
    }

    private protected override void HazardUpdate()
    {
        if (_distanceThreshold > DistanceToNode())
        {
            UpdateCurrentNodeIndex();
        }

        GoToNode();
    }

    private void UpdateCurrentNodeIndex()
    {
        if (_routeType == RouteType.Return)
        {
            ReturnThroughNodes();
        }
        else
        {
            LoopThroughNodes();
        }
    }

    private void LoopThroughNodes()
    {
        if (_currentNodeIndex == _nodes.Length - 1)
        {
            _currentNodeIndex = 0;
        }
        else
        {
            _currentNodeIndex++;
        }
    }

    private void ReturnThroughNodes()
    {
        if (_currentNodeIndex == _nodes.Length - 1)
        {
            _goingBack = true;
        }
        else if (_currentNodeIndex == 0)
        {
            _goingBack = false;
        }

        if (_goingBack)
        {
            _currentNodeIndex--;
        }
        else
        {
            _currentNodeIndex++;
        }
    }

    private float DistanceToNode()
    {
        return Vector2.Distance(transform.position, _currentNode);
    }

    private float DistanceToNode(int nodeIndex)
    {
        return Vector2.Distance(transform.position, _nodes[nodeIndex]);
    }

    private void GoToNode()
    {
        Vector2 direction = (Vector3)_currentNode - transform.position;
        _rigidbody2D.velocity = direction.normalized * _linearSpeed;
    }
}
