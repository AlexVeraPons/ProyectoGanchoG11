using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curosr : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorPressedTexture;
    private Texture2D _cursorDefaultTexture;

    private void Start()
    {
        Cursor.visible = false;
        _cursorDefaultTexture = this.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
    }

    private void Update()
    {
        // this position to mouse position
        this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y ,0);

        //if mouse is down change texture to pressed until it is released
        if (Input.GetMouseButton(0))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(_cursorPressedTexture, new Rect(0, 0, _cursorPressedTexture.width, _cursorPressedTexture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(_cursorDefaultTexture, new Rect(0, 0, _cursorDefaultTexture.width, _cursorDefaultTexture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
