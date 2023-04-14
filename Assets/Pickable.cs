using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable, IDropable
{
    public Action<GameObject> OnPickupAction;
    public Action OnDroppedAction;

    public void Pick(GameObject obj)
    {
        OnPickupAction?.Invoke(obj);
    }

    public void Drop()
    {
        OnDroppedAction?.Invoke();
    }
}
