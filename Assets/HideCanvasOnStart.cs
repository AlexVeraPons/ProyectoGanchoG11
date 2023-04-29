using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvasOnStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
