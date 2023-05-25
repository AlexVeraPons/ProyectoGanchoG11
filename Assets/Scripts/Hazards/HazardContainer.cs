using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HazardContainer
{
    public HazardContainer() { }

    [SerializeField]
    [Tooltip(
        "The time after which the hazard will start, after the last slot has finished its duration."
    )]
    private float _startTime;
    public float StartTime => _startTime;

    [SerializeField]
    [Tooltip(
        "Ignore the duration of the previous Container, and start this one immediately after the start time has elapsed."
    )]
    private bool _ignorePreviousDuration;

    public bool IgnorePreviousDuration => _ignorePreviousDuration;

    [SerializeField]
    public List<Hazard> Hazards = new List<Hazard>();

    public void StartContainer()
    {
        foreach (Hazard hazard in Hazards)
        {
            hazard.HazardStart();
        }
    }

    public void ResetContainer()
    {
        foreach (Hazard hazard in Hazards)
        {
            hazard.ResetHazard();
        }
    }

    public float GetDuration()
    {
        float duration = 0;

        foreach (Hazard hazard in Hazards)
        {
            if (hazard.Duration > duration)
            {
                duration = hazard.Duration;
            }
        }

        return duration;
    }
}
