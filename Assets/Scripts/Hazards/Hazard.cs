using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    [SerializeField]
    private protected float _startTime = 0;

    [SerializeField]
    private protected float _duration = 0;

    private bool _running = false;

    private void Start()
    {
        StartCoroutine(StartAfterDelay());
        ComponentsDisabeler();
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(seconds: _startTime);
        StartRunning();
        yield return new WaitForSeconds(seconds: _duration);
        StopRunning();
    }

    private void Update()
    {
        if (!_running)
        {
            return;
        }

        HazardUpdate();
    }

    private void StartRunning()
    {
        ComponentEnebaler();
        Appear();
        _running = true;
    }

    private void StopRunning()
    {
        _running = false;
        Disappear();
    }

    private protected void ComponentsDisabeler()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    private protected void ComponentEnebaler()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private protected abstract void HazardUpdate();

    private protected abstract void Appear();

    private protected abstract void Disappear();
}
