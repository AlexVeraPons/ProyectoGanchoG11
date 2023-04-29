using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    [SerializeField] float _nextWaveTimeThreshold = 3;
    [Space(10)]
    private float _currentTime;
    [SerializeField] InputAction _nextWaveDebugAction;
    [SerializeField] InputAction _resetWaveDebugAction;
    [SerializeField] private List<GameObject> _waves = new List<GameObject>();

    public static Action OnWaveStart;
    public static Action OnLoadWave;
    public static Action OnWavesRestarted;
    
    int _currentWave = 0;

    void Awake()
    {
        _nextWaveDebugAction.Enable();
        _resetWaveDebugAction.Enable();

        foreach(var newStage in GetComponentsInChildren<Wave>())
        {
            _waves.Add(newStage.gameObject);
        }
    }

    void OnEnable()
    {
        //PlayerHealth.OnPlayerDeath += ResetWaves;
    }

    void OnDisable()
    {
        //PlayerHealth.OnPlayerDeath -= ResetWaves;
    }

    void Start()
    {
        UnloadAllWaves();
        Begin();
    }

    void Update()
    {
        if(_currentTime < _nextWaveTimeThreshold)
        {
            _currentTime += Time.deltaTime;
        }
        else if(CanIncreaseWave())
        {
            GoToNextWave();
            _currentTime = 0;
        }

        //Debug stuff
        if(_resetWaveDebugAction.triggered)
        {
            ResetWaves();
            print("Current Wave: " + _currentWave);
        }

        if(_nextWaveDebugAction.triggered)
        {
            GoToNextWave();
            print("Current Wave: " + _currentWave);
        }
    }

    void UnloadAllWaves()
    {
        foreach(var stage in _waves)
        {
            stage.SetActive(false);
        }
    }

    void Begin()
    {
        OnWaveStart?.Invoke();
        LoadCurrentWave();
    }

    void LoadCurrentWave()
    {
        _waves[_currentWave].SetActive(true);
        OnLoadWave?.Invoke();
    }

    void GoToNextWave()
    {
        _currentWave += 1;
        LoadCurrentWave();
        //UnloadPreviousWave(); Comentado porque son oleadas
    }

    void UnloadPreviousWave()
    {
        _waves[_currentWave - 1].SetActive(false);
    }

    void LoadAllWaves()
    {
        foreach(var stage in _waves)
        {
            stage.SetActive(true);
        }
    }

    bool CanIncreaseWave()
    {
        if(_currentWave < _waves.Count - 1)
        {
            return true;
        }

        return false;
    }

    void ResetWaves()
    {
        LoadAllWaves();
        OnWavesRestarted?.Invoke();
        _currentWave = 0;
        UnloadAllWaves();
        Begin();
    }
}
