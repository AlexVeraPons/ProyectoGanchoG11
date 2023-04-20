public class HookablePlatform : HookableObject
{
    //This inherited class will make the hook retrieve the owner to where it collides

    public override void OnHook(Hook hook)
    {
        hook.SwitchState(HookState.RetrievingOwner);
    }
}

