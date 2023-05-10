using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCanvasOnDeath : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    [SerializeField]
    private Button _restartButton;

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
        _canvasGroup.interactable = true;
        _restartButton.Select();
    }
}
