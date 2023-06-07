using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Hazard
{
    public static Action OnCollected;
    private bool _collected;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _originalPos;
    private const float _speed = 100f;
    [SerializeField] private Transform _playerTransform;

    private protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private protected override void Appear()
    {
        _shouldDespawn = false;
        _originalPos = this.transform.position;
    }

    private protected override void Disappear()
    {
        
    }

    private protected override void HazardUpdate()
    {

    }

    public override void ResetHazard()
    {
        _collected = false;
        this.transform.position = _originalPos;
        base.ResetHazard();
    }

    private void FixedUpdate()
    {
        if(_collected == true)
        {
            _rigidbody2D.velocity = (_playerTransform.position - this.transform.position).normalized * _speed * Time.deltaTime;
        }
    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_running)
            return;

        if(collision.CompareTag("Player") == true
        && WaveManager._instance.NextWaveIsNotNull())
        {
            _collected = true;
            _animator.Play("Collected");
            OnCollected?.Invoke();
        }
    }
}
