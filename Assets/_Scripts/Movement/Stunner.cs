using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour, IStunneable
{
    [SerializeField]
    private float _stunTime = 1f;
    [SerializeField]
    private Mover _mover;
    public void Stun()
    {
        _mover.Stun();
        StartCoroutine(StunTimer());
        Debug.Log("Stunned");
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(_stunTime);
        _mover.Unstun();
    }

    public void Unstun()
    {
        Debug.Log("Unstunned");
        _mover.Unstun();
    }
}
