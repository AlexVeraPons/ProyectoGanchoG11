using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnStoreObject : MonoBehaviour
{
    ObjectStorer _objectStorer;

    public float Score => _currentScore;
    [SerializeField] float _currentScore = 0;
    [SerializeField] float _scoreMultiplier = 1f;

    private void Awake()
    {
        _objectStorer = GetComponent<ObjectStorer>();
    }
    private void Update()
    {
        if (HasCollectedObjects())
        {
            IncrementScore();
        }
    }
    
    bool HasCollectedObjects()
    {
        return _objectStorer.PickableList.Count > 0;
    }

    void IncrementScore()
    {
        _currentScore += Time.deltaTime * _scoreMultiplier;
    }

    void ResetScore()
    {
        _currentScore = 0f;
    }
}
