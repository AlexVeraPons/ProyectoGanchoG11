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

    private Rigidbody2D _rigidbody2D;
    private GameObject[] _nodes;
    private int _currentNodeIndex;
    private GameObject _currentNode => _nodes[_currentNodeIndex];

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _nodes = new GameObject[transform.childCount];
    }

    private protected override void Appear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _nodes[i] = transform.GetChild(i).gameObject;
        }
    }

    private protected override void HazardUpdate() 
    {
        GoToNode();
    }

    private void GoToNode()
    {
        throw new NotImplementedException();
    }

    private protected override void Disappear() { }
}
