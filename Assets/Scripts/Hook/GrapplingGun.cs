using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    // Input Action references
    [SerializeField] InputActionReference _hookInputReference;
    [SerializeField] InputActionReference _movementInputReference; // Pending change

    // Referenced values in inspector
    [SerializeField] GameObject _hookObject;
    [SerializeField] GrapplingGunStatus _status = GrapplingGunStatus.unused;
    [SerializeField] float _peakDistance = 10f;
    [SerializeField] float _grabDistance = 0.5f;
    
    // Public references
    public float PeakDistance => _peakDistance;
    public float GrabDistance => _grabDistance;
    
    // Private references
    private Vector2 _lastInput = Vector2.right;
    Hook _hook;

    // Unity functions
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
        if (MovementInputTriggered() == true)
        {
            if (MovementInput() != Vector2.zero)
            {
                AssignLastInput();
            }
        }

        if (CanLaunchSelf() == true && HasPressedButton() == true)
        {
            LaunchHook();
        }
    }

    // In here there's all the bool logic
    bool MovementInputTriggered()
    {
        return _movementInputReference.action.triggered;
    }
    bool HasPressedButton()
    {
        return _hookInputReference.action.triggered;
    }
    bool CanLaunchSelf()
    {
        return _status == GrapplingGunStatus.unused;
    }

    // In here there's all the vector2 logic (Only used to read the values of the input movement)
    Vector2 MovementInput()
    {
        return _movementInputReference.action.ReadValue<Vector2>();
    }

    // In here there's functions called every frame
    void AssignLastInput()
    {
        _lastInput = _movementInputReference.action.ReadValue<Vector2>();
    }

    // In here there's functions only called once
    private void LaunchHook()
    {
        _hook.gameObject.SetActive(true);
        _status = GrapplingGunStatus.used;
        _hook.Launch(position: this.transform.position, newDirection: _lastInput);
    }
    public void ResetSelf()
    {
        _status = GrapplingGunStatus.unused;
    }

    //In here there's all the assignment logic, only called once ever
    private void SetGrapplingGunOwner()
    {
        _hook.AssignGrapplingGun(this);
    }
}

public enum GrapplingGunStatus
{
    unused,
    used
}