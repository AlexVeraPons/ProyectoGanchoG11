using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class BackgroundSpawner : MonoBehaviour
{
    public Action OnShoot;
    // [SerializeField]
    // private GameObject _bullet;
    [Space(10)]

    [Header("TIME related")]
    [Space(5)]

    [SerializeField]
    [Tooltip("time it takes to spawn the next wave")]
    private float _spawnTime;
    private float _time;

    void Start()
    {
        _spawnTime = 2f;
        _time = _spawnTime;


    }

    void Update()
    {
        Timer();
    }
    void Timer()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            Shoot();
            ResetTimer();
        }
    }
    void Shoot()
    {
        OnShoot?.Invoke();
        Debug.Log("OnShoot");
    }
    void ResetTimer()
    {
        _time = _spawnTime;
    }
}