using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimOnCollection : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _animationName;

    private void OnEnable()
    {
        Collectible.OnCollected += PlayAnimation;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= PlayAnimation;  
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
