using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    ObjectStorer _objectStorer;

    [SerializeField] bool _usesTrigger;

    private void Awake()
    {
        _objectStorer = GetComponent<ObjectStorer>();
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(_usesTrigger == false)
        {
            if(IsPickable(obj.collider, "Object"))
            {
                PickObject(obj.collider);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(_usesTrigger == true)
        {
            if (IsPickable(obj, "Object"))
            {
                PickObject(obj);
            }
        }
    }

    bool IsPickable(Collider2D col, string tag)
    {
        return col.CompareTag(tag) && col.gameObject.GetComponent<IPickable>() != null;
    }

    void PickObject(Collider2D col)
    {
        col.gameObject.GetComponent<IPickable>().Pick(this.gameObject);
        if (_objectStorer != null) { _objectStorer.AddToList(col.gameObject); }
    }
}
