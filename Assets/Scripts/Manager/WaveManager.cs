using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
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
        if(_resetWaveDebugAction.triggered)
        {
            ResetWaves();
        }

        if(_nextWaveDebugAction.triggered)
        {
            GoToNextLevel();
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

    void GoToNextLevel()
    {
        if(CanIncreaseWave() == true)
           {
            _currentWave += 1;
            LoadCurrentWave();
            UnloadPreviousWave();
           }
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
        if(_currentWave < _waves.Count)
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
