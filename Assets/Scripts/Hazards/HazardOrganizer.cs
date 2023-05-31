using System;
using System.Collections;
using UnityEngine;

public class HazardOrganizer : MonoBehaviour
{
    [SerializeField]
    private HazardContainer[] _hazardContainers;
    private HazardContainer[] _temporaryHazardContainers;
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
        Debug.Log(this.transform.parent.name+" Started");
    }

    private void Start()
    {
        PopulateListFromChildren();
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

        _temporaryHazardContainers = new HazardContainer[numberOfContainers];
        numberOfContainers = 0;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<HazardContainerObject>() != null)
            {
                HazardContainerObject HazardContainerObject =
                    child.GetComponent<HazardContainerObject>();
                _temporaryHazardContainers[numberOfContainers] = new HazardContainer();
                PopulateHazardsFromContainer(HazardContainerObject, numberOfContainers);
                numberOfContainers++;
            }
        }

        FillHazardContainerFromTemporaryContainers();
    }

    private void FillHazardContainerFromTemporaryContainers()
    {
        for (int i = 0; i < _temporaryHazardContainers.Length; i++)
        {
            bool same = true;
            //same hazards in the same container
            if(_temporaryHazardContainers[i].Hazards.Count == _hazardContainers[i].Hazards.Count)
            {
                for (int j = 0; j < _temporaryHazardContainers[i].Hazards.Count; j++)
                {
                    if(_temporaryHazardContainers[i].Hazards[j] != _hazardContainers[i].Hazards[j])
                    {
                        same = false;
                    }
                }

                if(same)
                {
                    _temporaryHazardContainers[i].StartTime = _hazardContainers[i].StartTime;
                    _temporaryHazardContainers[i].IgnorePreviousDuration = _hazardContainers[i].IgnorePreviousDuration;
                }
            }

        
            //resize _hazardContainers
            Array.Resize(ref _hazardContainers, _temporaryHazardContainers.Length);


            _hazardContainers[i] = _temporaryHazardContainers[i];
        }
    }

    private void PopulateHazardsFromContainer(
        HazardContainerObject hazardContainerObject,
        int containerIndex = 0
    )
    {
        foreach (Transform child in hazardContainerObject.transform)
        {
            if (child.GetComponent<Hazard>() != null)
            {
                _temporaryHazardContainers[containerIndex].Hazards.Add(
                    child.GetComponent<Hazard>()
                );
            }
            else
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.GetComponent<Hazard>() != null)
                    {
                        _temporaryHazardContainers[containerIndex].Hazards.Add(
                            grandchild.GetComponent<Hazard>()
                        );
                    }
                }
            }
        }
    }
}
