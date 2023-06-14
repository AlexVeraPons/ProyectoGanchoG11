using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseImage : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;

    [SerializeField]
    private float _offset = 15;

    PlayerInput _myInput;
    void Start()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_offset, _offset), CursorMode.Auto);

        _myInput = new PlayerInput();
        //_myInput.
    }

    void Update()
    {
        //_myInput.
    }
}
