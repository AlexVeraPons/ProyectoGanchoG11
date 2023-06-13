using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NextWaveOnHit : Hazard
{
    public static Action BossHit;
    public static Action BossTired;
    private Vector2 _originalPos;
    private Transform _playerTransform;

    private protected override void Awake()
    {
        base.Awake();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private protected override void Appear()
    {
        BossTired?.Invoke();
        _shouldDespawn = false;
        _running = true;
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
        //_hasBeenCollected = false;
        this.transform.position = _originalPos;
        base.ResetHazard();
    }

    private void FixedUpdate()
    {
        
    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_running)
        {
            return;
        }

        if(collision.GetComponent<HookBehaviour>() == true
        && WaveManager._instance.NextWaveIsNotNull())
        {
            Debug.Log("NextWaveOnHit");
            BossHit?.Invoke();
        }

    }
}
