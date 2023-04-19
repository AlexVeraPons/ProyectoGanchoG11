using UnityEngine;

public abstract class HookableObject : MonoBehaviour, IHookable
{
    abstract public void OnHook(Hook hook);
}

