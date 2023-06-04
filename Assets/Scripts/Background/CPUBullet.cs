using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CPUBullet : MonoBehaviour
{
    private Cannons _cannons;
    private Dispersion _dispersion;

    private enum State{goingForward, goingSideways}
    private State state;
    private Rigidbody2D _rigidbody;

    private Vector2 _originalDirection;
    private Vector2 _direction;

    private int _speed = 2;

    private float _baseTimer = 0.5f;
    private float timer;

    void Start()
    {
        _cannons = GetComponentInParent<Cannons>();
        _dispersion = GetComponentInParent<Dispersion>();

        _rigidbody = GetComponent<Rigidbody2D>();

        _direction = _cannons.GetDirection();

        timer = _baseTimer;
        state = State.goingForward;
        
    }

    void Update()
    {
        _rigidbody.velocity = _direction.normalized * _speed;
        Timer();
    }
    void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Rotate();
            ResetTimer();
            Debug.Log("timer");
        }
    }
    void ResetTimer()
    {
        timer = _baseTimer;
    }
    void Rotate()
    {
        Debug.Log("rotate");
        _direction = Quaternion.Euler(0,0, GetRandomNumber(-60, 60)) * _direction;
    }
    int GetRandomNumber(int firstNumber, int secondNumber){
        //int number = UnityEngine.Random.Range(-1, 1);
        if(UnityEngine.Random.value > 0.5){
            //Debug.Log("firstNumber");
            //Debug.Log(firstNumber);
            return firstNumber;
        }else if(UnityEngine.Random.value < 0.5){
            //Debug.Log("secondNumber");
            //Debug.Log(secondNumber);
            return secondNumber;
        }else{
            //Debug.Log("referfunction");
            GetRandomNumber(firstNumber, secondNumber);
        }
        //Debug.Log("final else");
        return secondNumber;
    }
}