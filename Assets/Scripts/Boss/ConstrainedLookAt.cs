using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainedLookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Vector2 _targetPosition => _target.position;

    [SerializeField]
    private float _radius;

    [SerializeField]
    private float _imageRadius;

    private Vector2 _initialVector;

    private void Start()
    {
        _initialVector = this.transform.position;
    }

    private void Update()
    {
        this.transform.position = GetPositionInRadius();
    }

    private Vector2 GetPositionInRadius()
    {
        Vector2 direction = _targetPosition - _initialVector;
        float distance = direction.normalized.magnitude * (_radius - _imageRadius);
        return _initialVector + direction.normalized * distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, _imageRadius);
    }
}
