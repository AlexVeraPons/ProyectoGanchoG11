using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private protected float _glitchDuration = 0;

    [SerializeField]
    [Tooltip("The total duration of the hazard.")]
    private protected float _duration = 0;
    public float Duration => _duration;

    private protected bool _shouldDespawn = true;

    private protected bool _running = false;

    private protected bool _hasrun = false;

    private GlitchController _glitchController;

    private protected virtual void Awake()
    {
        if (GetComponent<GlitchController>() != null)
        {
            _glitchController = GetComponent<GlitchController>();
        }
    }

    private void Start()
    {
        ComponentDisabler();
    }

    public void HazardStart()
    {
        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        Appear();
        GenerateUniqueSound();
        ComponentEnabler();

        yield return new WaitForSeconds(seconds: _glitchDuration);
        StartRunning();

        if (!_shouldDespawn)
        {
            yield break;
        }

        yield return new WaitForSeconds(seconds: _duration - _glitchDuration);
        Disappear();
        StopRunning();
    }

    private bool IsTimeToStart()
    {
        throw new NotImplementedException();
    }

    private protected void Update()
    {
        if (!_running)
        {
            return;
        }

        HazardUpdate();
    }

    private virtual protected void StartRunning()
    {
        _hasrun = true;
        PlayRunSound();
        _running = true;
    }

    private virtual protected void StopRunning()
    {
        StopRunSound();
        _running = false;
    }

    private protected void ComponentDisabler()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private protected void ComponentEnabler()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_running)
            return;

        if (collision.GetComponent<IDamageable>() != null)
        {
            Debug.Log("Hazard hit " + collision.gameObject.name);
            DamageableAction(collision);
        }
    }

    private protected virtual void DamageableAction(Collider2D collision)
    {
        collision.GetComponent<IDamageable>().TakeDamage(1);
    }

    private protected virtual void GenerateUniqueSound()
    {
        //This should be the only thing here
        AudioManager._instance.PlaySingleSound(SingleSound.EnemyAppear);
    }

    /// <summary>
    /// This method is called to generate a swound.
    /// </summary>
    private protected virtual void PlayRunSound()
    {
        AudioManager._instance.PlaySingleSound(SingleSound.EnemyAppear);
    }

    /// <summary>
    /// This method is called to stop a sound. Only use this if the generated sound is looped.
    /// </summary>
    private protected virtual void StopRunSound() { }

    /// <summary>
    /// This method is called then the hazard is running.
    /// </summary>
    private protected abstract void HazardUpdate();

    /// <summary>
    /// This method is called when the hazard starts.
    /// </summary>
    private protected virtual void Appear()
    {
        if (_glitchController != null)
        {
            _glitchController.Glitch(_glitchDuration);
        }
    }

    /// <summary>
    /// This method is called when the hazard stops.
    /// </summary>
    private protected virtual void Disappear()
    {
        ComponentDisabler();
    }

    /// <summary>
    /// This method is called when the hazard has to be reset
    /// </summary>
    public virtual void ResetHazard()
    {
        _hasrun = false;
        StopRunning();
        Disappear();
    }

    internal bool IsFinished()
    {
        // only returns false if it has already been started
        return _hasrun && !_running;
    }
}
