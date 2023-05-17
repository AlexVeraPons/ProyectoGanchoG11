using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    public static WaveManager _instance;

    public WaveState State => _state;
    private WaveState _state;

    public WorldCollector Collector => _collector;
    private WorldCollector _collector;
    private WaveSpawner _spawner;

    public static Action OnUnloadWave;
    public static Action OnLoadWave;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] InputActionReference _debugNextWaveAction;

    [SerializeField] bool _respawnOnWave;
    [SerializeField] bool _inProgress = false;
    [SerializeField] int _currentWaveID = 0;
    [SerializeField] int _currentWorldID = 0;
    [SerializeField] float _timeBetweenWaves = 1f;

    private void Awake()
    {
        _collector = GetComponent<WorldCollector>();
        _spawner = GetComponent<WaveSpawner>();

        if(_instance == null) { _instance = this; }
        else { Destroy(this.gameObject); }

        AssignIDs();

        //_debugNextWaveAction.action.Enable();
    }

    void OnEnable()
    {
        Collectible.OnCollected += NextWave;
    }

    void OnDisable()
    {
        Collectible.OnCollected -= NextWave;
    }

    private void Start()
    {
        _spawner.DespawnAll(this._collector);
        _spawner.SpawnWave(this._collector, _currentWorldID, _currentWaveID);

        OnLoadWave?.Invoke();
    }

    private void Update()
    {
        //if(_debugNextWaveAction.action.triggered) { NextWave(); }

        switch(_state)
        {
            case WaveState.Pending:
            break;

            case WaveState.Ongoing:
            break;

            case WaveState.Finished:
            break;
        }
    }

    public void NextWave()
    {
        if(_inProgress == false)
        {
            _inProgress = true;

            Wave nextWave = GetWaveByID(_currentWaveID + 1);
            World currentWorld = GetWorldByID(_currentWorldID);
            if(nextWave != null)
            {
                WavePacket previousPacket = new WavePacket(_currentWorldID, _currentWaveID);

                if (NextWaveIsInAnotherWorld(currentWorld, nextWave) == true)
                {
                    _currentWorldID += 1;
                }

                _currentWaveID += 1;

                WavePacket nextPacket = new WavePacket(_currentWorldID, _currentWaveID);
                StartCoroutine(Next(previousPacket, nextPacket));
            }
        }
    }

    public IEnumerator Next(WavePacket previousPacket, WavePacket nextPacket)
    {
        _playerTransform.position = GetWaveByID(nextPacket.GetWaveID()).SpawnPosition;

        OnUnloadWave?.Invoke();

        this._spawner.DespawnWave(_collector, previousPacket.GetWorldID(), previousPacket.GetWaveID());
        yield return new WaitForSeconds(_timeBetweenWaves);
        this._spawner.SpawnWave(_collector, nextPacket.GetWorldID(), nextPacket.GetWaveID());

        OnLoadWave?.Invoke();

        _inProgress = false;
    }

    public void SwitchState(WaveState newState)
    {
        _state = newState;
        OnEnterState(newState);
    }

    void OnEnterState(WaveState newState)
    {
        switch(newState)
        {
            default:

                break;
        }
    }

    void AssignIDs()
    {
        int newWorldID = 0;
        int newWaveID = 0;
        foreach(World world in _collector.Worlds)
        {
            foreach(Wave wave in world.WaveList)
            {
                wave.SetID(newWaveID);

                newWaveID += 1;
            }

            world.SetID(newWorldID);

            newWorldID += 1;
        }

        print("Worlds: " + newWorldID);
        print("Waves: " + newWaveID);
    }

    Wave GetWaveByID(int ID)
    {
        foreach(World world in _collector.Worlds)
        {
            foreach(Wave wave in world.WaveList)
            {
                if(wave.ID == ID)
                {
                    return wave;
                }
            }
        }

        Debug.LogError("The Wave ID exceeds the actual ammount!");
        return null;
    }

    World GetWorldByID(int ID)
    {
        foreach(World world in _collector.Worlds)
        {
            if(world.ID == ID)
            {
                return world;
            }
        }

        Debug.LogError("The World ID exceeds the actual ammount!");
        return null;
    }

    bool NextWaveIsInAnotherWorld(World currentWorld, Wave newWave)
    {
        foreach(Wave wave in currentWorld.WaveList)
        {
            if(wave == newWave)
            {
                return false;
            }
        }

        return true;
    }
}

public enum WaveState
{
    Pending,
    Ongoing,
    Finished
}

public class WavePacket
{
    int _worldID;
    int _waveID;


    public WavePacket(int worldID, int waveID)
    {
        this._worldID = worldID;
        this._waveID = waveID;
    }

    public int GetWorldID() { return _worldID; }
    public int GetWaveID() { return _waveID; }
} 