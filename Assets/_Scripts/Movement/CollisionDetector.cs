using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Transform _groundCheckTransform;
    [SerializeField]
    private float _groundCheckRadius = 1f;

    [Header("Wall Check")]
    [SerializeField]
    private LayerMask _whatIsWall;

    [SerializeField]
    private Transform _wallCheckTransform;
    [SerializeField]
    private float _wallCheckRadius = 1f;


    public bool IsGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(
            _groundCheckTransform.position,
            _groundCheckRadius,
            _whatIsGround
        );
        return colliders.Length > 0;
    }

    private void Update()
    {
        if (IsTouchingWall())
        {
            Debug.Log("Touching Wall");
        }
        if (IsTouchingInfront())
        {
            Debug.Log("Touching Infront");
        }
    }

    public bool IsTouchingWall()
    {
        var colliders = Physics2D.OverlapCircleAll(
            _wallCheckTransform.position,
            _wallCheckRadius,
            _whatIsWall
        );
        return colliders.Length > 0;
    }

    public bool IsTouchingInfront()
    {
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
        Gizmos.color = Color.red;
        DrawGizmosSphere(_groundCheckTransform.position, _groundCheckRadius);
        DrawGizmosSphere(_wallCheckTransform.position, _wallCheckRadius);
    }

    private void DrawGizmosSphere(Vector2 position, float checkRadious = 1f)
    {
        Gizmos.DrawWireSphere(position, checkRadious);
    }
}
