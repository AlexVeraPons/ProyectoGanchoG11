using System;
using System.Collections;
using UnityEngine;

public class DeathZone : HazardWithWarning
{
    [Space(10)]
    [Header("Growth")]
    [SerializeField]
    private float _growthSpeed = 0;

    bool _firstFrameHorizontalPlayed = false;
    bool _firstFrameVerticalPlayed = false;

    private Vector2 _initialScale;

    private protected override void Awake()
    {
        base.Awake();
        _initialScale = this.transform.localScale;
    }

    private protected override void WarningFinished() { }

    private protected override void AfterWarningUpdate()
    {
        Grow();
    }

    private void Grow()
    {
        StartCoroutine(GrowX());
    }

    private IEnumerator GrowX()
    {
        if (_firstFrameHorizontalPlayed == false)
        {
            AudioManager._instance.PlaySingleSound(SingleSound.HazardAreaHorizontalScale);
            _firstFrameHorizontalPlayed = true;
        }

        if (this.transform.localScale.x >= _warningZone.transform.localScale.x - 0.1f)
        {
            StartCoroutine(GrowY());
            StopCoroutine(GrowX());
        }

        var xScale = Mathf.Lerp(
            this.transform.localScale.x,
            _warningZone.transform.localScale.x,
            _growthSpeed * Time.deltaTime
        );

        this.transform.localScale = new Vector3(xScale, this.transform.localScale.y, 1);

        yield return new WaitUntil(
            () => this.transform.localScale.x >= _warningZone.transform.localScale.x - 0.1f
        );
    }

    private IEnumerator GrowY()
    {
        yield return null;

        if (_firstFrameVerticalPlayed == false)
        {
            AudioManager._instance.PlaySingleSound(SingleSound.HazardAreaVerticalScale);
            _firstFrameVerticalPlayed = true;
        }

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

    public override void ResetHazard()
    {
        base.ResetHazard();
        _firstFrameHorizontalPlayed = false;
        _firstFrameVerticalPlayed = false;
        this.transform.localScale = _initialScale;
    }
}
