using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeathCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToSpawn;

    [SerializeField]
    private Transform _ObjectsGoHere;

    private int _maxDeaths = 50;

    private int _currentDeaths = 0;

    [SerializeField]
    private Transform[] _points;
    private int pointer = 0;

    private float _offset = 0;
    private bool isMultiple;   

    void Start()
    {
        isMultiple = _currentDeaths % 5 == 0;
        Vector2 position = _points[0].position - _points[1].position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentDeaths++;
            SpawnCounter();
        }
    }

    private void SpawnCounter()
    {
        if (_currentDeaths < _maxDeaths)
        {

            Debug.Log(_currentDeaths);

            Instantiate(_objectToSpawn, new Vector3(_points[pointer].position.x + (_offset), _points[pointer].position.y, _points[pointer].position.z), Quaternion.Euler(0, 0, 70), _points[pointer]);
            _offset++;

            CheckToChangePos();
        }
    }
    private void CheckToChangePos()
    {
        if (isMultiple)
        {
            MoveToNewPoint();
        }
    }
    private void MoveToNewPoint()
    {
        pointer++;
        Debug.Log(pointer);
        ResetOffset();
    }
    private void ResetOffset()
    {
        _offset = 0;
    }
}
