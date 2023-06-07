using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] LayerMask _affectedLayer;
    [SerializeField] float _strength;

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(_affectedLayer == (1 << other.gameObject.layer))
        {
            var rigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                AudioManager._instance.PlaySingleSound(SingleSound.PlatformBounce);
                _animator.Play("Bounce");
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(transform.right * _strength);
            }
        }
    }
}
