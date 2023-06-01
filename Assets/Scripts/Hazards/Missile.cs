using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrailRenderer))]
public class Missile : Hazard
{
    [Space(10)]
    [Header("Missile Variables")]
    [SerializeField]
    private float _rotationalSpeed;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _target;
    private Vector3 _targetPos => _target.transform.position;

    [SerializeField]
    private LayerMask _whatIsWall;

    [SerializeField]
    private float _collisionArea = 0;

    private Rigidbody2D _rigidbody2D;
    private TrailRenderer _trailRenderer;
    private Vector2 _intialPosition;
    private Quaternion _initialRotation;

    private protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _target = GameObject.FindObjectOfType<PlayerEntity>().gameObject;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Start()
    {
        _intialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    private protected override void Appear()
    {
        base.Appear();

        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.enabled = true;
    }

    private protected override void Disappear()
    {
        base.Disappear();

        if (_trailRenderer != null)
        {
            _trailRenderer.enabled = false;
        }
    }

    private protected override void HazardUpdate()
    {
        Vector2 direction = (Vector2)_targetPos - _rigidbody2D.position;
        direction.Normalize();

        Vector3 rotationAmount = Vector3.Cross(direction, transform.up);

        _rigidbody2D.angularVelocity = -_rotationalSpeed * rotationAmount.z;

        _rigidbody2D.velocity = transform.up * _speed;

        if (IsTouchingWall())
        {
            Disappear();
            StopRunning();
        }
    }

    private bool IsTouchingWall()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _collisionArea, _whatIsWall);
        return colliders.Length > 0;
    }

    private protected override void DamageableAction(Collider2D collision)
    {
        base.DamageableAction(collision);
        AudioManager._instance.PlaySingleSound(SingleSound.MissileCrash);
    }

    public override void ResetHazard()
    {
        transform.position = _intialPosition;
        transform.rotation = _initialRotation;
        base.ResetHazard();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _collisionArea);
    }

    private void OnTriggerEntered2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageableAction(collision);
        }
    }
}
