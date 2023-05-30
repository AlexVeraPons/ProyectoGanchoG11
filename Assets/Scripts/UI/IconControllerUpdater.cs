using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IconControllerUpdater : MonoBehaviour
{
    //This should me refactored and moved onto a manager that checks what is the last input and determines what you are using

    Image _image;
    [SerializeField] InputActionReference _controller;

    [SerializeField] Sprite _mouseSprite, _keyboardSprite, _controllerSprite;

    Vector2 _lastMousePosition;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (MouseIsActive() == true)
        {
            _image.sprite = _mouseSprite;
        }

        if (KeyboardIsActive() == true)
        {
            _image.sprite = _keyboardSprite;
        }

        if (ControllerIsActive() == true)
        {
            _image.sprite = _controllerSprite;
        }
    }

    bool MouseIsActive()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (_lastMousePosition != mousePos)
        {
            _lastMousePosition = mousePos;
            return true;
        }

        _lastMousePosition = mousePos;
        return false;
    }

    bool KeyboardIsActive()
    {
        return Keyboard.current.anyKey.wasPressedThisFrame;
    }

    bool ControllerIsActive()
    {
        return _controller.action.triggered;
    }
}
