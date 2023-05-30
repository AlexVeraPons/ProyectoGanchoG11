using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunAnimatorUpdater : MonoBehaviour
{
    private Animator _animator;
    private GrapplingGun _gun;

    private void Awake()
    {
        _gun = GetComponent<GrapplingGun>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_gun.State == GrapplingGunState.Waiting)
        {
            _animator.SetBool("isUsingTongue", false);
        }
        else if (_gun.State == GrapplingGunState.InProgress)
        {
            _animator.SetBool("isUsingTongue", true);
        }
    }
}
