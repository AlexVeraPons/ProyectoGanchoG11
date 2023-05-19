using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Tooltip("The maximum strength of the shake")]
    [SerializeField] private int _magnitude = 5;
    [Tooltip("The time it takes to finish the screen shake")]
    [SerializeField] private float _timeToFinish = 1;
    
    ShakeState _state = ShakeState.Inactive;

    private float _timer = 0f;

    private Vector3 _referencePosition;

    void OnEnable()
    {
        LifeComponent.OnDeath += StartShake;
        WaveManager.OnUnloadWave += StartShake;
    }

    void OnDisable()
    {
        LifeComponent.OnDeath -= StartShake;
        WaveManager.OnUnloadWave -= StartShake;
    }

    void Start()
    {
        _referencePosition = this.transform.position;
    }

    void Update()
    {
        if(_state == ShakeState.InProgress)
        {
            _timer += Time.deltaTime;
            
            int x = Random.Range(-1, 1);
            int y = Random.Range(-1, 1);

            Vector3 vectorDirectior = new Vector3(x, y, 0).normalized;

            float actualStrength = Mathf.Lerp(_magnitude, 0, _timer / _timeToFinish);

            this.transform.position = _referencePosition + new Vector3(vectorDirectior.x * actualStrength, vectorDirectior.y * actualStrength, 0);


            if(_timer > _timeToFinish)
            {
                _timer = 0f;
                this.transform.position = _referencePosition;
                _state = ShakeState.Inactive;
            }
        }   
    }

    void StartShake()
    {
        _state = ShakeState.InProgress;
    }
}

public enum ShakeState
{
    Inactive,
    InProgress,
}
