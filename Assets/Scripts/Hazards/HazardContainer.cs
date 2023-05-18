using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HazardContainer
{
    public HazardContainer() { }

    [SerializeField]
    [Tooltip("The time after which the hazard will start, after the last slot has finished.")]
    private float _starTime;
    public float StartTime => _starTime;

    [SerializeField]
    private List<Hazard> _hazards = new List<Hazard>();

    public void StartContainer()
    {
        foreach (Hazard hazard in _hazards)
        {
            hazard.HazardStart();
        }
    }

    public void ResetContainer()
    {
        foreach (Hazard hazard in _hazards)
        {
            hazard.ResetHazard();
        }
    }

    public float GetDuration()
    {
        float duration = 0;
        foreach (Hazard hazard in _hazards)
        {
            if (hazard.Duration > duration)
            {
                duration = hazard.Duration;
            }
        }
        return duration;
    }
}

