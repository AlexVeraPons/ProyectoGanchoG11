using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnInteraction : MonoBehaviour, IInteractable
{
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoInteraction()
    {
        _spriteRenderer.color = Color.green;
    }

    public void UndoInteraction()
    {
        _spriteRenderer.color = Color.white;
    }
}
