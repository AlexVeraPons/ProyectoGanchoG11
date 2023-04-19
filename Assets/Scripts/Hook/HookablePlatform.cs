public class HookablePlatform : HookableObject
{
    public override void OnHook(Hook hook)
    {
        hook.SwitchState(HookState.RetrievingSelf);
    }
}

