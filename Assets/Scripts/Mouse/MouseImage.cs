using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseImage : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;

    [SerializeField]
    private float _offset = 15;
    void Start()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_offset, _offset), CursorMode.Auto);
    }

    void Update()
    {
        
    }
}
