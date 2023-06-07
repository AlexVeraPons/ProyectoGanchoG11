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
    private bool _isActive => _animator.GetBool("IsActive");
    private Collider2D _collider;
    private Animator _animator;

    private void OnEnable() {
        LifeComponent.OnDeath += Activate;
    }

    private void OnDisable() {
        LifeComponent.OnDeath -= Activate;
    }

    private void Start() {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();    
    }

     public void UndoInteraction()
    {
        Debug.Log("UndoInteraction");
        if (_isActive) {
            Deactivate();
            ActivateAfterCooldown();
        }
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
        _animator.SetBool("IsActive", false);
        _collider.enabled = false;
    }

    private void Activate()
    {
        Debug.Log("Activate");
        _animator.SetBool("IsActive", true);
        _collider.enabled = true;
    }

    private void ActivateAfterCooldown() {
        StartCoroutine(ActivateAfterCooldownCoroutine());
     }

    private IEnumerator ActivateAfterCooldownCoroutine()
    {
        yield return new WaitForSeconds(_cooldown);
        Activate();
    }
   
    public void DoInteraction()
    {
        //throw new NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
