using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateByReference : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Transform _parentTransform;

    [SerializeField] ScriptableVector2 _vector;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _parentTransform = gameObject.transform.parent.transform;
    }

    private void Update()
    {
        if(_vector.Value != Vector2.zero)
        {
            _spriteRenderer.enabled = true;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(_vector.Value.y, _vector.Value.x), Vector3.forward);
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}
