using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimDelayLoadWave : MonoBehaviour
{
    [SerializeField] string _animationName;
    private Animator _animator;
    [SerializeField] float _minDelayTime, _maxDelayTime;

    private void OnEnable()
    {
        WaveManager.OnLoadWave += LoadAnimationWithDelay;
    }

    private void OnDisable()
    {
        WaveManager.OnLoadWave -= LoadAnimationWithDelay;
    }

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        if (_minDelayTime > _maxDelayTime
        || _minDelayTime == _maxDelayTime
        || _minDelayTime < 0)
        {
            StartCoroutine(LoadAnimation(0));
        }
        else
        {
            StartCoroutine(LoadAnimation(Random.Range(_minDelayTime, _maxDelayTime)));
        }
    }

    void LoadAnimationWithDelay()
    {
        StartCoroutine(LoadAnimation(Random.Range(_minDelayTime, _maxDelayTime)));
    }

    IEnumerator LoadAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.Play(_animationName);
    }
}
