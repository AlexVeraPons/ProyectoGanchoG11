using System;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    public Action OnOverridingMovement;
    public Action OnStopOverridingMovement;

    [SerializeField]
    private HookMovement _hookMovement;

    [SerializeField]
    private float _speed;
    private Transform _targetTransform;
    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _hookMovement.OnHookedTransform += SetTransform;
        _hookMovement.OnHookReleased += UnsetTransform;
    }

    private void OnDisable()
    {
        _hookMovement.OnHookedTransform -= SetTransform;
        _hookMovement.OnHookReleased -= UnsetTransform;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_targetTransform == null)
            return;
        MoveTowardsTransform(GetTargetTransform());
    }

    private void SetTransform(Transform transform)
    {
        _targetTransform = transform;
        OnOverridingMovement?.Invoke();
    }

    private void UnsetTransform()
    {
        _targetTransform = null;
        OnStopOverridingMovement?.Invoke();
    }

    private Vector3 GetTargetTransform()
    {
        return _targetTransform.position;
    }

    private void MoveTowardsTransform(Vector3 vector)
    {
        _rigidbody2D.MovePosition(
            Vector3.MoveTowards(transform.position, vector, _speed * Time.deltaTime)
        );
    }
}
