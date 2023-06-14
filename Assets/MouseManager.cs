using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public static MouseManager _instance;

    [SerializeField] Texture2D _mouseNotClickedTexture, _mouseClickedTexture;
    Vector2 _hotspot = new Vector2(113, 45);

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }    
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(MouseIsClicked())
        {
            Cursor.SetCursor(_mouseClickedTexture, _hotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_mouseNotClickedTexture, _hotspot, CursorMode.Auto);
        }
    }

    bool MouseIsClicked()
    {
        return Mouse.current.leftButton.ReadValue() == 1;
    }
}
