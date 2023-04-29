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
    [Tooltip("The warning zone that will be displayed before the box grows.")]
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
        Debug.Log("DeathZone Disappear");
        _growing = false;
        _warningZone.SetActive(false);
        this.gameObject.SetActive(false);
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
        // FIRST GROW IN X THEN IN Y
        StartCoroutine(GrowX());
    }

    private IEnumerator GrowX()
    {
        var xScale = Mathf.Lerp(
            this.transform.localScale.x,
            _warningZone.transform.localScale.x,
            _growthSpeed * Time.deltaTime
        );

        this.transform.localScale = new Vector3(xScale, this.transform.localScale.y, 1);


        if (this.transform.localScale.x >= _warningZone.transform.localScale.x - 0.1f)
        {
            StartCoroutine(GrowY());
            StopCoroutine(GrowX());
        }

        yield return new WaitUntil(
            () => this.transform.localScale.x >= _warningZone.transform.localScale.x - 0.1f
        );
    }

    private IEnumerator GrowY()
    {
        yield return null;

        var yScale = Mathf.Lerp(
            this.transform.localScale.y,
            _warningZone.transform.localScale.y,
            _growthSpeed * Time.deltaTime
        );

        this.transform.localScale = new Vector3(this.transform.localScale.x, yScale, 1);

        yield return new WaitUntil(
            () => this.transform.localScale.y >= _warningZone.transform.localScale.y - 0.1f
        );
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        Debug.Log("DeathZone OnTriggerEnter2D");

        if (other.GetComponent<IKillable>() != null)
        {
            other.GetComponent<IKillable>().Kill();
        }
    }
}
