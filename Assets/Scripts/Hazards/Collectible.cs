using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Hazard
{
    public static Action OnCollected;

    private protected override void Appear()
    {
        _shouldDespawn = false;
    }

    private protected override void Disappear()
    {
        
    }

    private protected override void HazardUpdate()
    {

    }

    private protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_running)
            return;

        if(collision.CompareTag("Player") == true
        && WaveManager._instance.NextWaveIsNotNull())
        {
            OnCollected?.Invoke();
        }
    }
}
