using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollectible : Collectible
{
    public static Action OnCollectedBoss;

    private protected override void OnTriggerEnter2D(Collider2D other) {
        if(!_running)
        {
            return;
        }

        if(other.CompareTag("Player") == true)
        {
            OnCollectedBoss?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
