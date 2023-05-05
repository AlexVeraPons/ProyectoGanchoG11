using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    [SerializeField] LayerMask _affectedLayer;
    [SerializeField] float _strength;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(_affectedLayer == (1 << other.gameObject.layer))
        {
            var rigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(transform.right * _strength);
            }
        }
    }
}
