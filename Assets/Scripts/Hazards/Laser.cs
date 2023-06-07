using System;
using UnityEngine;

public class Laser : HazardWithWarning
{
    [SerializeField]
    private float _horizontalSpeedGrowth = 0;

    // initial scale
    private Vector3 _initialScale;

    private protected override void Awake()
    {
        base.Awake();
        _warningZone.SetActive(false);
        _initialScale = this.transform.localScale;
    }

    private protected override void WarningFinished()
    {
        AudioManager._instance.PlaySingleSound(SingleSound.AfterLaser);
    }

    private protected override void AfterWarningUpdate()
    { 
        GrowX();
    }

    private protected override void GenerateUniqueSound()
    { 
        AudioManager._instance.PlaySingleSound(SingleSound.BeforeLaser);
    }


    private void GrowX()
    {
        var xScale = Mathf.Lerp(
            this.transform.localScale.x,
            _warningZone.transform.localScale.x,
            _horizontalSpeedGrowth * Time.deltaTime
        );

        this.transform.localScale = new Vector3(xScale, this.transform.localScale.y, 1);
    }

    public override void ResetHazard()
    {
        base.ResetHazard();
        this.transform.localScale = _initialScale;
    }
}
