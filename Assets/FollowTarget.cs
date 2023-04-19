using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //[SerializeField] bool _enabled;
    [SerializeField] Vector2 _offset;
    [SerializeField] Transform _target;

    private void OnEnable()
    {
        gameObject.GetComponent<Pickable>().OnPickupAction += AssignTransform;
        gameObject.GetComponent<Pickable>().OnDroppedAction += UnnasignTransform;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Pickable>().OnPickupAction -= AssignTransform;
        gameObject.GetComponent<Pickable>().OnDroppedAction -= UnnasignTransform;
    }

    void AssignTransform(GameObject obj)
    { 
        transform.SetParent(obj.transform);
        _target = obj.transform;
        SetNewPosition();
    }
    void SetNewPosition() 
    { 
        transform.position = _target.position + (Vector3)_offset;
    }
    void UnnasignTransform(GameObject obj)
    {
        transform.SetParent(null);
    }
}
