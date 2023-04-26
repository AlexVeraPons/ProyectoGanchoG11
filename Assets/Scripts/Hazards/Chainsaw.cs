using System;
using UnityEngine;

public class Chainsaw : Hazard
{
    [SerializeField]
    private float _linearSpeed = 0;

    [SerializeField]
    private float _angularRotationalSpeed = 0;

    [Header("Hide Before Commit")]
    [SerializeField]
    private float _distanceThreshold;

    [SerializeField]
    private Transform _nodesParent;
    private Rigidbody2D _rigidbody2D;
    private Vector2[] _nodes;
    private int _currentNodeIndex;
    private Vector2 _currentNode => _nodes[_currentNodeIndex];

    private void Awake()
    {
        _nodes = new Vector2[_nodesParent.childCount + 1];
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private protected override void Appear()
    {
        _nodes[0] = this.transform.position;

        for (int i = 0; i < _nodesParent.childCount; i++)
        {
            _nodes[i+1] = _nodesParent.GetChild(i).gameObject.transform.position;
        }

        _currentNodeIndex = 0;
    }

    private protected override void HazardUpdate()
    {
        if (_distanceThreshold > DistanceToNode())
        {
            UpdateCurrentNodeIndex();
        }

        GoToNode();
        Rotate();
    }

    private void Rotate()
    {
        _rigidbody2D.angularVelocity = _angularRotationalSpeed;
    }

    private void UpdateCurrentNodeIndex()
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

    private float DistanceToNode()
    {
        return Vector2.Distance(transform.position, _currentNode);
    }

    private void GoToNode()
    {
        Vector2 direction = (Vector3)_currentNode - transform.position;
        _rigidbody2D.velocity = direction.normalized * _linearSpeed;
    }

    private protected override void Disappear()
    {
        this.gameObject.SetActive(false);
    }
}
