using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] InputActionReference _hookInputReference;
    [SerializeField] InputActionReference _movementInputReference;
    [SerializeField] GameObject _hookObject;
    Hook _hook;

    public float PeakDistance => _peakDistance;
    [SerializeField] float _peakDistance = 10f;

    public float GrabDistance => _grabDistance;
    [SerializeField] float _grabDistance = 0.5f;

    [SerializeField] GrapplingGunStatus _status = GrapplingGunStatus.unused;

    private void Awake()
    {
        _hook = _hookObject.GetComponent<Hook>();
    }

    private void Start()
    {
        SetGrapplingGunOwner();
    }

    private void Update()
    {
        if(CanLaunchSelf() == true && HasPressedButton() == true)
        {
            LaunchHook();
        }
    }

    bool HasPressedButton()
    {
        return _hookInputReference.action.triggered;
    }

    bool CanLaunchSelf() 
    { 
        return _status == GrapplingGunStatus.unused;
    }

    private void LaunchHook()
    {
        _hook.gameObject.SetActive(true);
        _status = GrapplingGunStatus.used;
        Vector2 dir = _movementInputReference.action.ReadValue<Vector2>();
        _hook.Launch(position: this.transform.position, newDirection: dir);
    }

    private void SetGrapplingGunOwner()
    {
        _hook.AssignGrapplingHook(this);
    }

    public void ResetSelf()
    {
        _status = GrapplingGunStatus.unused;
    }
}

public enum GrapplingGunStatus
{
    unused,
    used
}