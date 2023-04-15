using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable, IDropable
{
    public GameObject CurrentPicker => _currentPicker;
    [SerializeField] GameObject _currentPicker;

    public Action<GameObject> OnPickupAction;
    public Action<GameObject> OnDroppedAction;

    public void Pick(GameObject obj)
    {
        if(_currentPicker != obj && _currentPicker != null) { _currentPicker.GetComponent<ObjectStorer>().RemoveFromList(this.gameObject); }
        _currentPicker = obj;
        OnPickupAction?.Invoke(obj);
    }

    public void Drop(GameObject obj)
    {
        OnDroppedAction?.Invoke(obj);
    }
}
