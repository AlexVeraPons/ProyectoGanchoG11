using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    public int ID => _worldID;
    private int _worldID;
    public List<Wave> WaveList => _waveList;
    [SerializeField] private List<Wave> _waveList;
    [SerializeField] private RespawnType _respawnType = RespawnType.Wave;

    private void Awake()
    {
        Wave[] waves = GetComponentsInChildren<Wave>();
        _waveList = waves.ToList<Wave>();
    }

    public void SetID(int id)
    {
        _worldID = id;
    }

    //Should be used
    public int GetFirstWaveID()
    {
        return _waveList[0].ID;
    }

    /// <summary>
    /// Sets the returning bool on true if it respawns on waves, false if it respawns on worlds.
    /// </summary>
    public bool SetRespawnType()
    {
        if(_respawnType == RespawnType.Wave)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum RespawnType
{
    Wave,
    World
}