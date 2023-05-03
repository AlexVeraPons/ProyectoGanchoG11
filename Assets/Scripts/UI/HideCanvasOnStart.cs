using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvasOnStart : MonoBehaviour
{
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }
}
