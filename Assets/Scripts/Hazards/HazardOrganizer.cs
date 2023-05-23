using System;
using System.Collections;
using UnityEngine;

public class HazardOrganizer : MonoBehaviour
{
    [SerializeField]
    private HazardContainer[] _hazardContainers;
    private int _currentContainerIndex = 0;
    private HazardContainer _currentContainer => _hazardContainers[_currentContainerIndex];

    private void OnEnable()
    {
        WaveManager.OnLoadWave += LevelStarted;
        WaveManager.OnUnloadWave += ResetContainers;
        WaveManager.OnResetWave += ResetContainers;
        WaveManager.OnResetWave += ResetContainerIndex;
    }

    private void OnDisable()
    {
        WaveManager.OnLoadWave -= LevelStarted;
        WaveManager.OnUnloadWave -= ResetContainers;
        WaveManager.OnResetWave -= ResetContainers;
        WaveManager.OnResetWave -= ResetContainerIndex;
    }

    private void LevelStarted()
    {
        StartCoroutine(StartInitialContainer());
    }

    private IEnumerator StartInitialContainer()
    {
        yield return new WaitForSeconds(seconds: _hazardContainers[0].StartTime);
        _currentContainer.StartContainer();
        StartCoroutine(NextContainerAfterDelay(_currentContainer.GetDuration()));
    }

    private IEnumerator NextContainerAfterDelay(float duration = 0)
    {
        if (_hazardContainers[_currentContainerIndex + 1] != null)
        {
            if (_hazardContainers[_currentContainerIndex + 1].IgnorePreviousDuration)
            {
                duration = 0;
            }
        }

        yield return new WaitForSeconds(seconds: duration);

        if (_currentContainerIndex == _hazardContainers.Length - 1)
        {
            yield break;
        }

        yield return new WaitForSeconds(
            seconds: _hazardContainers[_currentContainerIndex + 1].StartTime
        );

        NextContainer();
    }

    private void NextContainer()
    {
        _currentContainerIndex++;
        _currentContainer.StartContainer();

        if (_currentContainerIndex == _hazardContainers.Length - 1)
        {
            return;
        }

        if (_currentContainer.IgnorePreviousDuration)
        {
            StartCoroutine(NextContainerAfterDelay());
            return;
        }

        StartCoroutine(NextContainerAfterDelay(_currentContainer.GetDuration()));
    }

    private void ResetContainers()
    {
        foreach (HazardContainer container in _hazardContainers)
        {
            container.ResetContainer();
        }
    }

    private void ResetContainerIndex()
    {
        _currentContainerIndex = 0;
    }
}
