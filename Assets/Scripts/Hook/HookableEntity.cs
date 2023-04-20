using UnityEngine;

public class HookableEntity : HookableObject
{
    //This inherited class will make this gameObject (needs a Rigidbody2D) susceptible to being grabbed
    //by a player's hook

    public override void OnHook(Hook hook)
    {
        if (hook.Owner != this.gameObject)
        {
            hook.SwitchState(newState: HookState.RetrievingTarget, gameObject: this.gameObject);
            hook.AssignTarget(this.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}