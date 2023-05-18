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
    }

    private void OnDisable()
    {
        WaveManager.OnLoadWave -= LevelStarted;
    }

    private void LevelStarted()
    {
        StartCoroutine(NextContainerAfterDelay());
    }

    private IEnumerator NextContainerAfterDelay()
    {
        yield return new WaitForSeconds(
            seconds: _hazardContainers[_currentContainerIndex + 1].StartTime
        );
        NextContainer();
    }

    private void NextContainer()
    {
        _currentContainerIndex++;
        _currentContainer.StartContainer();
    }
}
