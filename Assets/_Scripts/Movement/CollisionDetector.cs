using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private Transform _groundCheckTransform;
    [SerializeField]
    private float _checkRadius = 0.15f;
    public bool IsGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(
            _groundCheckTransform.position,
            _checkRadius,
            _whatIsGround
        );
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckTransform.position, _checkRadius);
    }
}
