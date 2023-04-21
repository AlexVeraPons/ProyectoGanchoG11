using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIButton : MonoBehaviour, IButton
{
    public abstract void OnPress();
}