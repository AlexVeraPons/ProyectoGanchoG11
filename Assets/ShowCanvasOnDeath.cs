using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasOnDeath : MonoBehaviour
{
    CanvasGroup _canvasGroup;

    private void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        LifeComponent.OnDeath += ShowUICanvasGroup;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= ShowUICanvasGroup;    
    }

    void ShowUICanvasGroup()
    {
        _canvasGroup.alpha = 1;
    }
}
