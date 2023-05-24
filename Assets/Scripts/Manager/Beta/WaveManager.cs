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
    public static Action OnResetWorld;
    public static Action OnUnloadWave;
    public static Action OnLoadWave;

    [Header("MANAGER VALUES")]
    [SerializeField]
    bool _respawnOnWave;

    [SerializeField]
    float _timeBetweenWaves = 0.3f;

    [Space(10)]
    [Header("DO NOT TOUCH")]
    [SerializeField]
    private Transform _playerTransform;
    bool _inProgress = false;
    int _currentWaveID = 0;
    int _currentWorldID = 0;

    private void Awake()
    {
        _collector = GetComponent<WorldCollector>();
        _spawner = GetComponent<WaveSpawner>();

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        AssignIDs();
    }

    void OnEnable()
    {
        Collectible.OnCollected += NextWave;

        if (_respawnOnWave == true)
        {
            LifeComponent.OnDeath += ResetWave;
        }
        else
        {
            LifeComponent.OnDeath += ResetWorld;
        }
    }

    void OnDisable()
    {
        Collectible.OnCollected -= NextWave;

        if (_respawnOnWave == true)
        {
            LifeComponent.OnDeath -= ResetWave;
        }
        else
        {
            LifeComponent.OnDeath -= ResetWorld;
        }
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
                WaveData previousWaveData = new WaveData(_currentWorldID, _currentWaveID);

                if (NextWaveIsInAnotherWorld(currentWorld, nextWave) == true)
                {
                    print("Next wave is in another world");
                    _currentWorldID += 1;
                    _respawnOnWave = GetWorldByID(_currentWorldID).SetRespawnType();
                }

                _currentWaveID += 1;

                WaveData nextWaveData = new WaveData(_currentWorldID, _currentWaveID);
                StartCoroutine(Next(previousWaveData, nextWaveData));
            }
            else
            {
                OnLoadWave?.Invoke();
            }
        }
    }

    public IEnumerator Next(
        WaveData previousPacket,
        WaveData nextWaveData,
        bool isRespawning = false
    )
    {
        OnUnloadWave?.Invoke();
        this._spawner.DespawnWave(
            _collector,
            previousPacket.GetWorldID(),
            previousPacket.GetWaveID()
        );
        yield return new WaitForSeconds(_timeBetweenWaves);
        this._spawner.SpawnWave(_collector, nextWaveData.GetWorldID(), nextWaveData.GetWaveID());

        ResetWavePlayerPosition(nextWaveData);

        if (isRespawning == true)
        {
            OnResetWave?.Invoke();
        }

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

    void ResetWave()
    {
        WaveData waveData = new WaveData(_currentWorldID, _currentWaveID);
        StartCoroutine(Next(waveData, waveData, isRespawning: true));
    }

    void ResetWorld()
    {
        WaveData waveData = new WaveData(
            _currentWorldID,
            this._collector.Worlds[_currentWorldID].WaveList[0].ID
        );

        StartCoroutine(Next(waveData, waveData, isRespawning: true));
    }

    void ResetWavePlayerPosition(WaveData waveData)
    {
        if (waveData.GetWorldID() > 0) //If there is a world ID
        {
            _playerTransform.position = GetWorldByID(waveData.GetWorldID()).WaveList[
                0
            ].SpawnPosition;
        }
        else
        {
            if (GetWaveByID(waveData.GetWaveID()).SpawnPosition != new Vector2(0, 0))
            {
                _playerTransform.position = GetWaveByID(waveData.GetWaveID()).SpawnPosition;
            }
        }
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

    public int GetWorldIDByWave(int waveID)
    {
        foreach (World world in _collector.Worlds)
        {
            foreach (Wave wave in world.WaveList)
            {
                if (wave.ID == waveID)
                {
                    return world.ID;
                }
            }
        }

        Debug.LogError("The wave ID is erroneous!");
        return -1;
    }
}
