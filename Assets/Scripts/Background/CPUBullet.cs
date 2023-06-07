using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CPUBullet : MonoBehaviour
{
    private Cannons _cannons;
    private Dispersion _dispersion;
    private int _dispersionNumber;
    private bool _inverted;

    private enum State { goingForward, goingSideways }
    private State state;
    private Rigidbody2D _rigidbody;

    private Vector2 _originalDirection;
    private Vector2 _direction;

    private int _speed = 2;

    private float _baseTimer; // = 0.5f
    private float _timer;

    void Start()
    {
        _cannons = GetComponentInParent<Cannons>();
        _inverted = _cannons.GetInverted();
        _dispersion = GetComponentInParent<Dispersion>();
        _dispersionNumber = _dispersion.GetDispersion();

        _rigidbody = GetComponent<Rigidbody2D>();

        _originalDirection = _cannons.GetDirection();

        _baseTimer = UnityEngine.Random.Range(0.3f, 1.5f);
        _timer = _baseTimer;
        state = State.goingForward;

    }

    void Update()
    {
        switch (state)
        {
            case State.goingForward:
                _rigidbody.velocity = _originalDirection.normalized * _speed;
                break;
            case State.goingSideways:
                _rigidbody.velocity = _direction.normalized * _speed;
                break;
        }
        Timer();
        CheckOutsideRange();
    }
    void Timer()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            ChangeState();
            ResetTimer();
        }
    }
    void ChangeState()
    {
        if (state == State.goingForward)
        {
            state = State.goingSideways;
            Rotate();
        }
        else
        {
            state = State.goingForward;
        }
    }
    void ResetTimer()
    {
        _timer = _baseTimer;
    }
    void Rotate()
    {
        if (_inverted)
        {
            _direction = Quaternion.Euler(0, 0, GetRandomNumber(45, -45)) * _originalDirection;

        }
        else if (_inverted == false)
        {
            _direction = Quaternion.Euler(0, 0, GetRandomNumber(-45, 45)) * _originalDirection;

        }
    }
    int GetRandomNumber(int firstNumber, int secondNumber)
    {
        int number = UnityEngine.Random.Range(1, 6); //ficate no de 1 a 6, sino de 1 a numero de cannions 6 = cannions + 1
        if (number > _dispersionNumber)
        {
            //esquerra
            return secondNumber;
        }
        else if (number <= _dispersionNumber)
        {
            //dreta
            return firstNumber;
        }
        else
        {
            GetRandomNumber(firstNumber, secondNumber);
        }
        return secondNumber;
    }
    private void CheckOutsideRange()
    {
        if (this.transform.position.x < -15)
        {
            DespawnBullet();
        }
        else if (this.transform.position.x > 15)
        {
            DespawnBullet();
        }
        else if (this.transform.position.y > 10)
        {
            DespawnBullet();
        }
        else if (this.transform.position.y < -10)
        {
            DespawnBullet();
        }

    }

    private void DespawnBullet()
    {
        state = State.goingForward;
        this.gameObject.SetActive(false);
    }
    public void SayHi()
    {
        _timer = _baseTimer;
    }
}