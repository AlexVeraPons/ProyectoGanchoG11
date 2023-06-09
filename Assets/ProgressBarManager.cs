using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager _instance;

    [SerializeField] Slider _slider;

    private float _percentageIncrease = 0f;

    private float _currentValue = 0f;

    private float _timer = 0f;
    [SerializeField] private bool _active = false;

    [SerializeField] private float _dampness = 1f;


    void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {
        WaveManager.OnUnloadWave += EmptyBar;
        WaveManager.OnLoadWave += Reset;
    }

    void OnDisable()
    {
        WaveManager.OnUnloadWave -= EmptyBar;
        WaveManager.OnLoadWave -= Reset;
    }

    void Update()
    {
        if(_active == true)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if(_timer < 1f)
        {
            _timer += Time.deltaTime * _dampness;
        }
        else
        {
            _timer = 1f;
            _active = false;
        }

        UpdateBar();
    }

    private void UpdateBar()
    {
        if(_timer <= 0.5f)
        {        
            _slider.value = Mathf.Lerp(_slider.value, _currentValue, _timer);
        }
        else
        {
            _slider.value = Mathf.SmoothStep(_slider.value, _currentValue, _timer);
        }
    }

    void SetNewPercentage(int ammount)
    {
        _percentageIncrease = _slider.maxValue / ammount;
    }

    public void Increase()
    {
        _currentValue = Math.Min(_currentValue + _percentageIncrease, _slider.maxValue);
        _timer = 0f;
        _active = true;
    }

    void Reset()
    {
        Wave wave = WaveManager._instance.GetWaveByID(WaveManager._instance.CurrentWaveID);
        int ammount = wave.gameObject.GetComponentInChildren<HazardOrganizer>().ContainerCount;
        if(ammount == 0)
        {
            Debug.Log("The loaded wave organizer has 0 containers?");
        }
        else
        {
            ResetBar(Math.Max(ammount - 1, 1));
        }
    }

    void EmptyBar()
    {
        _currentValue = 0f;
        _slider.value = 0f;
        _timer = 0f;
        _active = true;
    }

    void ResetBar(int ammount)
    {
        _currentValue = 0f;
        _slider.value = 0f;
        SetNewPercentage(ammount: ammount);
    }
}
