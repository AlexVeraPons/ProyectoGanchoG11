using System;
using UnityEngine;

public class Laser : HazardWithWarning
{
    [SerializeField]
    private float _horizontalSpeedGrowth = 0;

    private protected override void Awake()
    {
        base.Awake();
        _warningZone.SetActive(false);
    }

    private protected override void WarningFinished()
    {
    }

    private protected override void AfterWarningUpdate() { 
        GrowX();
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
}
