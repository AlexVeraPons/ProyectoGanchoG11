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
        if (_hazardContainers.Length == 0)
        {
            yield break;
        }
        yield return new WaitForSeconds(seconds: _hazardContainers[0].StartTime);
        _currentContainer.StartContainer();
        StartCoroutine(NextContainerAfterDelay(_currentContainer.GetDuration()));
    }

    private IEnumerator NextContainerAfterDelay(float duration = 0)
    {
        if (_currentContainerIndex < _hazardContainers.Length - 1)
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

    public void PopulateListFromChildren()
    {
        PopulateContainersFromChildren();
    }

    private void PopulateContainersFromChildren()
    {
        int numberOfContainers = 0;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<HazardContainerObject>() != null)
            {
                numberOfContainers++;
            }
        }

        _hazardContainers = new HazardContainer[numberOfContainers];
        numberOfContainers = 0;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<HazardContainerObject>() != null)
            {
                HazardContainerObject HazardContainerObject = child.GetComponent<HazardContainerObject>();
                _hazardContainers[numberOfContainers] = new HazardContainer();
                PopulateHazardsFromContainer(HazardContainerObject, numberOfContainers);
                numberOfContainers++;
            }
        }
    }

    private void PopulateHazardsFromContainer(HazardContainerObject hazardContainerObject, int containerIndex = 0)
    {
        foreach (Transform child in hazardContainerObject.transform)
        {
            if (child.GetComponent<Hazard>() != null)
            {
                _hazardContainers[containerIndex].Hazards.Add(child.GetComponent<Hazard>());
            }
            else
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.GetComponent<Hazard>() != null)
                    {
                        _hazardContainers[containerIndex].Hazards.Add(grandchild.GetComponent<Hazard>());
                    }
                }
            }
        }
    }
}


