using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("Movable Object Check")]
    [SerializeField]
    private LayerMask _whatIsMovableObject;

    [SerializeField]
    private Transform _movableObjectCheckTransform = null;
    [SerializeField]
    private float _movableObjectCheckRadius = 1f;

    [Header("Ground Check")]
    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Transform _groundCheckTransform = null;
    [SerializeField]
    private float _groundCheckRadius = 1f;

    [Header("Wall Check")]
    [SerializeField]
    private LayerMask _whatIsWall;

    [SerializeField]
    private Transform _wallCheckTransform = null;
    [SerializeField]
    private float _wallCheckRadius = 1f;


    public bool IsGrounded()
    {
        if (_groundCheckTransform == null) return false;

        var colliders = Physics2D.OverlapCircleAll(
            _groundCheckTransform.position,
            _groundCheckRadius,
            _whatIsGround
        );
        return colliders.Length > 0;
    }

    public bool IsTouchingWall()
    {
        if (_wallCheckTransform == null) return false;

        var colliders = Physics2D.OverlapCircleAll(
            _wallCheckTransform.position,
            _wallCheckRadius,
            _whatIsWall
        );
        return colliders.Length > 0;
    }

    public bool IsTouchingMovableObject()
    {
        if (_movableObjectCheckTransform == null) return false;

        var colliders = Physics2D.OverlapCircleAll(
            _movableObjectCheckTransform.position,
            _movableObjectCheckRadius,
            _whatIsMovableObject
        );
        return colliders.Length > 0;
    }

    public bool IsTouchingInfront()
    {
        if (_groundCheckTransform == null || _wallCheckTransform == null) return false;

        var collidersGround = Physics2D.OverlapCircleAll(
            _wallCheckTransform.position,
            _wallCheckRadius,
            _whatIsGround
        );

        var collidersWall = Physics2D.OverlapCircleAll(
            _wallCheckTransform.position,
            _wallCheckRadius,
            _whatIsWall
        );

        return collidersGround.Length > 0 || collidersWall.Length > 0;
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckTransform == null || _wallCheckTransform == null) return;

        Gizmos.color = Color.red;
        DrawGizmosSphere(_groundCheckTransform.position, _groundCheckRadius);
        DrawGizmosSphere(_wallCheckTransform.position, _wallCheckRadius);
    }

    private void DrawGizmosSphere(Vector2 position, float checkRadious = 1f)
    {
        Gizmos.DrawWireSphere(position, checkRadious);
    }
}
