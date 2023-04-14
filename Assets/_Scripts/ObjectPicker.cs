using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    [SerializeField] bool _usesTrigger;

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(_usesTrigger == false)
        {
            if (obj.collider.CompareTag("Object") && obj.gameObject.GetComponent<IPickable>() != null)
            {
                obj.gameObject.GetComponent<IPickable>().Pick(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(_usesTrigger == true)
        {
            if (obj.CompareTag("Object") && obj.gameObject.GetComponent<IPickable>() != null)
            {
                obj.gameObject.GetComponent<IPickable>().Pick(this.gameObject);
            }
        }
    }
}
