using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curosr : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        // this position to mouse position
        this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y ,0);
    }
}
