using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    RespawnType _respawnType;
    public RespawnType RespawnType => _respawnType;
    
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

        LifeComponent.OnDeath += Reset;

        /*
        if (_respawnType == RespawnType.Wave)
        {
        }
        else
        {
            LifeComponent.OnDeath += ResetWorld;
        }*/
    }

    void OnDisable()
    {
        Collectible.OnCollected -= NextWave;

        LifeComponent.OnDeath -= Reset;
    }

    private void Start()
    {
        _spawner.DespawnAll(this._collector);
        _spawner.SpawnWorld(this.Collector, _currentWorldID);
        _spawner.SpawnWave(this._collector, _currentWorldID, _currentWaveID);

        OnLoadWave?.Invoke();
    }

    public void NextWave()
    {
        if (_inProgress == false)
        {
            if(NextWaveIsNotNull() == true)
            {
                
                _inProgress = true;

                Wave nextWave = GetWaveByID(_currentWaveID + 1);
                World currentWorld = GetWorldByID(_currentWorldID);
                if (nextWave != null)
                {
                    WaveData previousWaveData = new WaveData(_currentWorldID, _currentWaveID);

                    if (WaveIsInAnotherWorld(currentWorld, nextWave) == true)
                    {
                        _currentWorldID += 1;

                        RespawnType newRespawnType = GetWorldByID(_currentWorldID).GetRespawnType();
                        _respawnType = newRespawnType;
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
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public IEnumerator Next
    (
        WaveData previousWaveData,
        WaveData nextWaveData,
        bool isRespawning = false
    )
    {
        OnUnloadWave?.Invoke();
        
        yield return new WaitForSeconds(_timeBetweenWaves);

        this._spawner.DespawnAllWaves(this._collector);

        Wave currentWave = GetWaveByID(_currentWaveID);
        World currentWorld = GetWorldByID(_currentWorldID);

        if (WaveIsInAnotherWorld(currentWorld, currentWave) == true)
        {
            this._spawner.DespawnWorld(_collector, previousWaveData.GetWorldID());
        }

        this._spawner.SpawnWave(_collector, nextWaveData.GetWorldID(), nextWaveData.GetWaveID());
        this._spawner.SpawnWorld(_collector, nextWaveData.GetWorldID());

        ResetWavePlayerPosition(nextWaveData);
        
        if(isRespawning == true)
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

    void Reset()
    {
        if(_respawnType == RespawnType.World)
        {
            _currentWaveID = GetWorldByID(GetWorldIDByWave(_currentWaveID)).WaveList[0].ID;
        }

        WaveData waveData = new WaveData(_currentWorldID, _currentWaveID);
        StartCoroutine(Next(waveData, waveData, isRespawning: true));
    }

    // void ResetWorld()
    // {
    //     WaveData waveData = new WaveData(
    //         _currentWorldID,
    //         this._collector.Worlds[_currentWorldID].WaveList[0].ID
    //     );

    //     StartCoroutine(Next(waveData, waveData, isRespawning: true));
    // }

    void ResetWavePlayerPosition(WaveData waveData)
    {
        _playerTransform.position = GetWaveByID(waveData.GetWaveID()).SpawnPosition;
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

        print("The Wave ID exceeds the actual ammount!");
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

    public bool NextWaveIsNotNull()
    {
        return GetWaveByID(_currentWaveID + 1) != null;
    }

    bool WaveIsInAnotherWorld(World currentWorld, Wave newWave)
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
