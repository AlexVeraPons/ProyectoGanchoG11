using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput PlayerInput => _playerInput;
    private PlayerInput _playerInput;
    public Action<Vector2> OnPlayerVectorInput;
    public Action OnPlayerJumpInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnPlayerVectorInput?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        OnPlayerJumpInput?.Invoke();
    }
}
