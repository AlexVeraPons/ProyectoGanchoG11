using UnityEngine;

public abstract class HookableObject : MonoBehaviour, IHookable
{
    //This inherited class is the basis for the hook colliding with something
    abstract public void OnHook(Hook hook);
}