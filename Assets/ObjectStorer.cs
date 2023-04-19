using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStorer : MonoBehaviour
{
    public List<GameObject> PickableList => _pickableObjects;
    [SerializeField] List<GameObject> _pickableObjects = new List<GameObject>();

    public void AddToList(GameObject obj)
    {
        _pickableObjects.Add(obj);
    }

    public void RemoveFromList(GameObject obj)
    {
        for (int i = _pickableObjects.Count - 1; i >= 0; i--)
        {
            if(obj == _pickableObjects[i])
            {
                _pickableObjects.RemoveAt(i);
            }
        }
    }
}
