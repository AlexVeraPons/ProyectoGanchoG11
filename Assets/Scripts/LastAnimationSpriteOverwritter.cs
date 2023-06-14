using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAnimationSpriteOverwritter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetLastSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
