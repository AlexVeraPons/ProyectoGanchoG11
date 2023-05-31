using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CPUBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Transform _originSquare;
    private GetSpawnerComponent _spawnerComponent;

    private Vector2 _originalDirection;
    private Vector2 _direction;

    private int _speed = 2;

    private float _baseTimer = 2;
    private float timer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spawnerComponent = GetComponent<GetSpawnerComponent>();
        //_originSquare = _spawnerComponent.GetTranso

        _originalDirection = this.transform.position - _originSquare.position;
        _direction = _originalDirection;
        _rigidbody.velocity = _originalDirection * _speed;

        timer = _baseTimer;
        Debug.Log(_originalDirection + "OGDir");
        Debug.Log(_direction+ "direction");
        Debug.Log(_originSquare.position + "motherPos");
        Debug.Log(this.transform.position + "bulletPos");
    }

    void Update()
    {
        _rigidbody.velocity = _direction * _speed;
        Timer();
        //Debug.Log(dir);
    }
    void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Rotate();
            ResetTimer();
            // Debug.Log("timer");
        }
    }
    void ResetTimer()
    {
        timer = _baseTimer;
    }
    void Rotate()
    {
        // Debug.Log("rotate");
        _direction = Quaternion.Euler(-60, 0, 0) * _direction;
        //Debug.Log(_direction);
    }
}
