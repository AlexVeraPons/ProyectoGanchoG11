using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker _instance;

    [Tooltip("The maximum strength of the shake")]
    [SerializeField] private float _magnitude = 5;
    [Tooltip("The time it takes to finish the screen shake")]
    [SerializeField] private float _timeToFinish = 1;
    
    private float _originalMagnitude;
    private float _originalTimeToFinish;

    ShakeState _state = ShakeState.Inactive;

    private float _timer = 0f;

    private Vector3 _referencePosition;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _referencePosition = this.transform.position;
        _originalMagnitude = _magnitude;
        _originalTimeToFinish = _timeToFinish;
    }

    void OnEnable()
    {
        LifeComponent.OnDeath += StartShake;
    }

    void OnDisable()
    {
        LifeComponent.OnDeath -= StartShake;
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

    public void StartShake()
    {
        _magnitude = _originalMagnitude;
        _timeToFinish = _originalTimeToFinish;
        _referencePosition = this.transform.position;

        _state = ShakeState.InProgress;
    }

    public void StartShake(float magnitude, float timeToFinish)
    {
        if(magnitude <= 0) { magnitude = _magnitude; } else { _magnitude = magnitude; }
        if(timeToFinish <= 0) { timeToFinish = _timeToFinish; } else { _timeToFinish = timeToFinish; }
        _referencePosition = this.transform.position;

        _state = ShakeState.InProgress;
    }
}

public enum ShakeState
{
    Inactive,
    InProgress,
}
