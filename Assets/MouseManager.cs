using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Texture2D _mouseClick, _mouseNotClick;

    Vector2 _offset = new Vector2(113, 113);

    private void Update() {
        if(Mouse.current.leftButton.ReadValue() == 1)
        {
            Cursor.SetCursor(_mouseClick, _offset, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_mouseNotClick, _offset, CursorMode.Auto);
        }
    }
}
