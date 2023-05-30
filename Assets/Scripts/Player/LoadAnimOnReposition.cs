using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimOnReposition : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _animationName;

    private void OnEnable()
    {
        WaveManager.OnMovePlayerPosition += PlayAnimation;
    }

    private void OnDisable()
    {
        WaveManager.OnMovePlayerPosition -= PlayAnimation;  
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
