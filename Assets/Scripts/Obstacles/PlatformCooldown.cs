using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlatformCooldown : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float _cooldown = 1f;
    [SerializeField]
    private float _offset = 0.3f;
    private bool _isActive => _animator.GetBool("IsActive");
    private Collider2D _collider;
    private Animator _animator;

    private void OnEnable() {
        WaveManager.OnResetWave += Activate;
    }

    private void OnDisable() {
        WaveManager.OnResetWave -= Activate;
    }

    private void Start() {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();    
    }

     public void UndoInteraction()
    {
        if (_isActive) {
            Deactivate();
            ActivateAfterCooldown();
        }
    }

    private void Deactivate()
    {
        _animator.SetBool("IsActive", false);
    }

    private void Activate()
    {
        _animator.SetBool("IsActive", true);
    }

    private void ActivateAfterCooldown() {
        StartCoroutine(ActivateAfterCooldownCoroutine());
     }

    private IEnumerator ActivateAfterCooldownCoroutine()
    {
        yield return new WaitForSeconds(_cooldown);
        Activate();
    }
   

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void DoInteraction()
    {
        // throw new NotImplementedException();
    }
}
