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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("hi");
            ChangeBackground();
        }
    }
    public void ChangeBackground(){
        Debug.Log("Check");
        _animation.SetTrigger("Start");
    }
}
