using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitioner : MonoBehaviour
{
    [SerializeField] private Material _screenTransitionMaterial;

    [SerializeField] private bool _active = false;

    [SerializeField] private float _delay = 0.25f;
    [SerializeField] private float _speedMultiplier = 1f;

    private float _transitionTime = 1f;
    private float _timer = 1f;
    private TransitionState _state = TransitionState.In;

    private string _propertyName = "_Progress";

    private void OnEnable()
    {
        Collectible.OnCollected += ActivateTransition;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= ActivateTransition;
    }

    private void Awake()
    {
        _timer = _transitionTime;
    }

    private void Update()
    {
        if(_active == true)
        {
            if(_state == TransitionState.In)
            {
                _timer -= Time.deltaTime * _speedMultiplier;
                _screenTransitionMaterial.SetFloat(_propertyName, _timer);
                if (_timer < 0)
                {
                    _timer = 0;
                    StartCoroutine(Delay());
                }
            }
            else
            {
                _timer += Time.deltaTime * _speedMultiplier;
                _screenTransitionMaterial.SetFloat(_propertyName, _timer);
                if (_timer > _transitionTime)
                {
                    _timer = 1;
                    _active = false;
                }   
            }
        }
    }

    private void ActivateTransition()
    {
        _timer = _transitionTime;
        _active = true;
        _state = TransitionState.In;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _state = TransitionState.Out;
    }

    public enum TransitionState
    {
        In,
        Out
    }
}
