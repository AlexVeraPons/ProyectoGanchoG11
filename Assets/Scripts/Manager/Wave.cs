using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour, IWave
{
    [SerializeField] float _waveTime = 1;
    public float WaveTime => _waveTime;
    /*
    public List<GameObject> Levels => _levels;
    [SerializeField] List<GameObject> _levels = new List<GameObject>();

    public void Awake()
    {
        foreach(var level in GetComponentsInChildren<Level>())
        {
            _levels.Add(level.gameObject);
        }
    }*/ 

    //Outdated el pinche sergio me ha cambiao los esquemas not really xd
}