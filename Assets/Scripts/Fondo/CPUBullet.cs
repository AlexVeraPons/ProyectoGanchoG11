using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CPUBullet : MonoBehaviour
{
    public enum DirectionState{goingForward, goingSideways}
    private Rigidbody2D _rigidbody;
    private Transform _originSquare;
    private GetSpawnerComponent _spawnerComponent;

    private Vector2 _originalDirection;
    private Vector2 _direction;

    private int _speed = 2;

    private float _baseTimer = 0.5f;
    private float timer;

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        //_spawnerComponent = GetComponent<GetSpawnerComponent>();
        //_originSquare = _spawnerComponent.GetTransform();

        _originalDirection = this.transform.position - new Vector3(0, 0, 0);
        _direction = _originalDirection;
        _rigidbody.velocity = _originalDirection * _speed;

        timer = _baseTimer;
        // Debug.Log(_originalDirection + "OGDir");
        // Debug.Log(_direction+ "direction");
        // Debug.Log(_originSquare.position + "motherPos");
        // Debug.Log(this.transform.position + "bulletPos");
    }

    void Update()
    {
        _rigidbody.velocity = _direction.normalized * _speed;
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
        _direction = Quaternion.Euler(GetRandomNumber(-60, 60), 0, 0) * _direction;
        //Debug.Log(_direction);
    }
    int GetRandomNumber(int firstNumber, int secondNumber){
        //int number = UnityEngine.Random.Range(-1, 1);
        if(UnityEngine.Random.value > 0.5){
            //Debug.Log("firstNumber");
            Debug.Log(firstNumber);
            return firstNumber;
        }else if(UnityEngine.Random.value < 0.5){
            //Debug.Log("secondNumber");
            Debug.Log(secondNumber);
            return secondNumber;
        }else{
            //Debug.Log("referfunction");
            GetRandomNumber(firstNumber, secondNumber);
        }
        //Debug.Log("final else");
        return secondNumber;
    }
}