using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateByReference : MonoBehaviour
{
    PlayerInput _input;

    Transform _parentTransform;

    [SerializeField] InputActionReference _aimInputReference;

    Vector2 _lastInput;

    private void Awake()
    {
        _parentTransform = gameObject.transform.parent.transform;
        _input = _parentTransform.GetComponent<PlayerInput>();

    }

    private void Update()
    {
        if(_input.currentControlScheme == "Keyboard&Mouse")
        {
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _parentTransform.transform.position).normalized;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x), Vector3.forward);
        }
        else
        {
            Vector2 direction = _aimInputReference.action.ReadValue<Vector2>();
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x), Vector3.forward);
        }
    }
}
