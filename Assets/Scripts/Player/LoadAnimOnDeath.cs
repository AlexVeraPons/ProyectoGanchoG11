using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimOnDeath : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _animationName;

    private void OnEnable()
    {
        LifeComponent.OnDeath += PlayAnimation;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= PlayAnimation;  
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void PlayAnimation()
    {
        _animator.Play(_animationName);
    }
}
