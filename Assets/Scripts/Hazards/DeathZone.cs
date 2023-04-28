using System;
using System.Collections;
using UnityEngine;

public class DeathZone : Hazard
{
    [SerializeField]
    [Tooltip("The time before the warning is displayed.")]
    private float _timeBeforeWarning = 0;

    [SerializeField]
    [Tooltip("The time the warning will be displayed before the box grows.")]
    private float _warningDuration = 0;

    [SerializeField]
    private float _growthSpeed = 0;

    [SerializeField]
    private GameObject _warningZone;

    private bool _growing = false;

    private void Awake()
    {
        _warningZone.SetActive(false);
    }

    private protected override void Appear()
    {
        _warningZone.SetActive(false);
        StartCoroutine(Warning());
    }

    private IEnumerator Warning()
    {
        yield return new WaitForSeconds(seconds: _timeBeforeWarning);
        ShowWarning();

        yield return new WaitForSeconds(seconds: _warningDuration);
        StartGrowing();
    }

    private void ShowWarning()
    {
        _warningZone.SetActive(true);
    }

    private void StartGrowing()
    {
        _growing = true;
    }

    private protected override void Disappear()
    {
        throw new System.NotImplementedException();
    }

    private protected override void HazardUpdate()
    {
        if (_growing == false)
        {
            return;
        }

        Grow();
    }

    private void Grow()
    {
        var xScale = Mathf.Lerp(
            this.transform.localScale.x,
            _warningZone.transform.localScale.x,
            _growthSpeed * Time.deltaTime
        );
        var yScale = Mathf.Lerp(
            this.transform.localScale.y,
            _warningZone.transform.localScale.y,
            _growthSpeed * Time.deltaTime
        );

        this.transform.localScale = new Vector3(xScale, yScale, 1);
    }

    private void OnTriggerEnter2D(Collider other) {
        if (other.GetComponent<IKillable>() != null)
        {
            other.GetComponent<IKillable>().Kill();
        }
    }
}
