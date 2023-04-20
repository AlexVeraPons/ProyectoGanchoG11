using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHookable
{
    //This interface is used when the hook collides with a class that has this interface
    
    void OnHook(Hook hook);
}
