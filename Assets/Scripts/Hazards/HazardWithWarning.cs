using System;
using System.Collections;
using UnityEngine;

public abstract class HazardWithWarning : Hazard
{
    [SerializeField]
    [Tooltip("The time before the warning is displayed.")]
    private protected float _timeBeforeWarning = 0;

    [SerializeField]
    [Tooltip("The time the warning will be displayed before the box grows.")]
    private protected float _warningDuration = 0;

    [Space(10)]
    [Header("References (DO NOT MODIFY)")]
    [SerializeField]
    [Tooltip("The warning zone that will be displayed before the box grows.")]
    private protected GameObject _warningZone;

    private bool _warningFinished = false;

    private void Awake()
    {
        _warningZone.SetActive(false);
    }

    private protected override void Appear()
    {
        base.Appear();
        
        _warningZone.SetActive(false);
        StartCoroutine(Warning());
    }

    private IEnumerator Warning()
    {
        yield return new WaitForSeconds(seconds: _timeBeforeWarning);
        ShowWarning();

        yield return new WaitForSeconds(seconds: _warningDuration);
        _warningFinished = true;
        WarningFinished();
    }

    /// <summary>
    /// This method is called when the warning has finished.
    /// </summary>
    private protected abstract void WarningFinished();

    private void ShowWarning()
    {
        _warningZone.SetActive(true);
    }

    private protected override void HazardUpdate()
    {
        if (_warningFinished == false)
        {
            return;
        }

        AfterWarningUpdate();
    }

    private protected override void Disappear()
    {
        _warningZone.SetActive(false);
        _warningFinished = false;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// This method is an update that only works after the warning is displayed.
    /// </summary>
    private abstract protected void AfterWarningUpdate();

    private new protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_running || !_warningFinished)
            return;

        if (collision.GetComponent<IKillable>() != null)
        {
            collision.GetComponent<IKillable>().Kill();
        }
    }
}
