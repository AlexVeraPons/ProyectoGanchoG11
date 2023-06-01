using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [Space(10)]

    [Header("TIME related")]
    [Space(5)]

    [SerializeField]
    [Tooltip("time it takes to spawn the next wave")]
    private float _spawnTime;
    private float _time;
    [SerializeField]
    private Transform[] _spawnPositions;
    void Start()
    {
        _spawnTime = 2f;
        _time = _spawnTime;

        //_spawnPositions = GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        UpdateSpawnersPosition();
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
        foreach (Transform objects in _spawnPositions)
        {
            Instantiate(_bullet, objects.transform.position, Quaternion.identity);
        }
    }
    void ResetTimer()
    {
        _time = _spawnTime;
    }
    void UpdateSpawnersPosition()
    {
        // for(int i=0; i<4; i++){
        //     Vector3 v = new Vector3(this.transform.position.x + 1/i+1, this.transform.position.y + 1/i+1, this.transform.position.z + 1/i+1);
        //     _spawnPositions[i] = v;
        // }
        /*
        foreach (Vector3 positions in _spawnPositions)
        {
            _spawnPositions[positions] = new Vector3(1, 2, 3);
        }
        */
    }

    // private void OnDrawGizmos()
    // {
    //     foreach (GameObject positions in _spawnPositions)
    //     {
    //         Gizmos.DrawSphere(positions.transform.position, 0.1f);

    //     }
    // }
}