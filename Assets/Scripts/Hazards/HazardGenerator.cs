using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGenerator : MonoBehaviour
{
    public List<GameObject> _hazardsToGenerate;
    [HideInInspector]
    public string[] _hazardNames;
    
    [HideInInspector]
    public int hazardIndx;
    private GameObject _hazardContainer;

    private GameObject[] _hazards;

    private void Start()
    {
        RegenereateGameObjects();
        RegenerateContainerGameObject();
    }

    private void RegenerateContainerGameObject()
    {
        GameObject[] hazardContainers = Resources.LoadAll<GameObject>("Container");
        _hazardContainer = hazardContainers[0];
    }

    private void RegenerateHazardList()
    {
        _hazards = Resources.LoadAll<GameObject>("Hazards");
        _hazardNames = new string[_hazards.Length];
        for (int i = 0; i < _hazards.Length; i++)
        {
            _hazardNames[i] = _hazards[i].name;
        }
    }

    public void RegenereateGameObjects()
    {
        RegenerateHazardList();
        RegenerateContainerGameObject();
    }

    public void AddHazardFromName(string name = null)
    {
        if (name == null)
        {
            name = _hazardNames[hazardIndx];
        }

        foreach (GameObject hazard in _hazards)
        {
            if (hazard.name == name)
            {
                _hazardsToGenerate.Add(hazard);
                break;
            }
        }
    }

    public void GenerateHazardsInNewContainer()
    {
        GameObject hazardContainer = Instantiate(_hazardContainer, transform.parent.transform);
        SetContainerName(hazardContainer);
        foreach (GameObject hazard in _hazardsToGenerate)
        {

            GameObject hazardInstance = Instantiate(hazard, hazardContainer.transform);
            hazardInstance.transform.localPosition = Vector3.zero;
        }

        _hazardsToGenerate.Clear();
    }

    private void SetContainerName(GameObject hazardContainer)
    {
        int containerIndex = 0;
        foreach (Transform child in transform.parent)
        {
            if (child.name.Contains("HazardContainer"))
            {
                containerIndex++;
            }
        }

        hazardContainer.name = "HazardContainer" + containerIndex;
    }
}
