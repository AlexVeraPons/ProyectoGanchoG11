using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundImage : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _speed = 10;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space");
            ChangeToBossBackground();
        }
    }
    private void ChangeToBossBackground()
    {
        _animator.SetTrigger("start");
        //_spriteRenderer.sprite = _bossBackground;
        Debug.Log("bossBackground");
    }
    private void ChangeToOriginalBackground()
    {
        //_spriteRenderer.sprite = _background;
        Debug.Log("OGbackground");

    }
}
