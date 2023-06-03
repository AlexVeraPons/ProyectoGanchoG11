using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private float _offset;

    [SerializeField]
    private float _speed = 0.1f;
    private Transform _target;
    private Vector2 _targetPosition => _target.position;
    private Vector2 _cameraTargetPosition;
    private void Start()
    {
        _target = GameObject.FindObjectOfType<PlayerEntity>().gameObject.transform;
    }

    private void Update()
    {
        // substract the offset by the target position vector normalized
        _cameraTargetPosition = _targetPosition / _offset;

        //follow the camera target position with a lerp, without affecting the z axis
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(_cameraTargetPosition.x, _cameraTargetPosition.y, this.transform.position.z), _speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_cameraTargetPosition, 0.5f);
    }
}
