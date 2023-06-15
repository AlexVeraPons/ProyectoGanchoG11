using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFondo : MonoBehaviour
{
    private Animator _animation;
    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    public void ChangeToBossBackground(){
        _animation.SetTrigger("Start");
    }
}
