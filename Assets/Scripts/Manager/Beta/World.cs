using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    public int ID => _worldID;
    private int _worldID;
    public List<Wave> WaveList => _waveList;
    [SerializeField] private List<Wave> _waveList;

    private void Awake()
    {
        Wave[] waves = GetComponentsInChildren<Wave>();
        _waveList = waves.ToList<Wave>();
    }

    public void SetID(int id)
    {
        _worldID = id;
    }
}