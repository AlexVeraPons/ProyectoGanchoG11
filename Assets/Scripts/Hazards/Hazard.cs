using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    [Header("Values")]

    [SerializeField]
    [Tooltip("The time after which the hazard will start.")]
    private protected float _startTime = 0;

    [SerializeField]
    [Tooltip("The total duration of the hazard.")]
    private protected float _duration = 0;

    private protected bool _running = false;

    private void OnEnable()
    {
        WaveManager.OnLoadWave += LevelStarted;
    }

    private void OnDisable()
    {
        WaveManager.OnLoadWave -= LevelStarted;
    }

    private void Start()
    {
        ComponentsDisabeler();
    }

    private void LevelStarted()
    {
        StartCoroutine(StartAfterDelay());
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

    private protected void OnTriggerEnter2D(Collider2D collision)
    {
      if(!_running) return;

        if(collision.GetComponent<IKillable>() != null)
        {
            collision.GetComponent<IKillable>().Kill();
        }
    }

    /// <summary>
    /// This method is called then the hazard is running.
    /// </summary>
    private protected abstract void HazardUpdate();

    /// <summary>
    /// This method is called when the hazard starts.
    /// </summary>
    private protected abstract void Appear();

    /// <summary>
    /// This method is called when the hazard stops.
    /// </summary>
    private protected abstract void Disappear();
}


