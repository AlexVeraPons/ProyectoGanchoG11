using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    public static WaveManager _instance;

    public WorldCollector Collector => _collector;
    private WorldCollector _collector;
    private WaveSpawner _spawner;

    public static Action OnResetWave;
    public static Action OnUnloadWave;
    public static Action OnLoadWave;

    [Header("MANAGER VALUES")]
    [SerializeField] bool _respawnOnWave;
    [SerializeField] float _timeBetweenWaves = 1f;

    [Space(10)]
    [Header("DO NOT TOUCH")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] bool _inProgress = false;
    [SerializeField] int _currentWaveID = 0;
    [SerializeField] int _currentWorldID = 0;

    private void Awake()
    {
        _collector = GetComponent<WorldCollector>();
        _spawner = GetComponent<WaveSpawner>();

        if (_instance == null) { _instance = this; }
        else { Destroy(this.gameObject); }

        AssignIDs();
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

    public void NextWave()
    {
        if (_inProgress == false)
        {
            _inProgress = true;

            Wave nextWave = GetWaveByID(_currentWaveID + 1);
            World currentWorld = GetWorldByID(_currentWorldID);
            if (nextWave != null)
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
        OnUnloadWave?.Invoke();

        this._spawner.DespawnWave(_collector, previousPacket.GetWorldID(), previousPacket.GetWaveID());
        yield return new WaitForSeconds(_timeBetweenWaves);
        this._spawner.SpawnWave(_collector, nextPacket.GetWorldID(), nextPacket.GetWaveID());

        OnLoadWave?.Invoke();

        _inProgress = false;
    }

    void AssignIDs()
    {
        int newWorldID = 0;
        int newWaveID = 0;
        foreach (World world in _collector.Worlds)
        {
            foreach (Wave wave in world.WaveList)
            {
                wave.SetID(newWaveID);

                newWaveID += 1;
            }

            world.SetID(newWorldID);

            newWorldID += 1;
        }
    }

    void ResetPlayerPosition()
    {
        _playerTransform.position = GetWaveByID(_currentWaveID).SpawnPosition;
    }

    Wave GetWaveByID(int ID)
    {
        foreach (World world in _collector.Worlds)
        {
            foreach (Wave wave in world.WaveList)
            {
                if (wave.ID == ID)
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
        foreach (World world in _collector.Worlds)
        {
            if (world.ID == ID)
            {
                return world;
            }
        }

        Debug.LogError("The World ID exceeds the actual ammount!");
        return null;
    }

    bool NextWaveIsInAnotherWorld(World currentWorld, Wave newWave)
    {
        foreach (Wave wave in currentWorld.WaveList)
        {
            if (wave == newWave)
            {
                return false;
            }
        }

        return true;
    }
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