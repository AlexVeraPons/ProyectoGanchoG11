using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public static MouseManager _instance;

    [SerializeField] Texture2D _mouseNotClickedTexture, _mouseClickedTexture;
    [SerializeField] Vector2 _hotspot;

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
            Cursor.SetCursor(_mouseClickedTexture, _hotspot, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(_mouseNotClickedTexture, _hotspot, CursorMode.ForceSoftware);
        }
    }

    bool MouseIsClicked()
    {
        return Mouse.current.leftButton.ReadValue() == 1;
    }
}
