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

    internal void StartContainer()
    {
        throw new NotImplementedException();
    }
}

