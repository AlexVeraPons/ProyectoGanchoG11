using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThowHook : MonoBehaviour
{
    public GameObject HookPrefab;
    public HookMovement hookMovement;
    public static Action<bool> hookIsMovingRight;
    public static Action<bool> hookCanMove;
    private bool _canThrow;

    public Vector2 InputtedDirection { get; private set; }

    void OnEnable()
    {
        HookMovement.OnHookEntersPlayer += EnableThrow;
    }
    void OnDisable()
    {
        HookMovement.OnHookEntersPlayer -= EnableThrow;

    }
    void Start()
    {
        _canThrow = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canThrow)
            {
                Throw();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(false);
            hookCanMove?.Invoke(true);
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hookMovement.IsMovingForward)
        {
            if (hitInfo.tag == "Hook")
            {
                DisableHook();
                Debug.Log("Hooked unactivated");
                EnableThrow(true);
            }
        }
    }
    void Throw()
    {
        EnableHook();
        hookIsMovingRight?.Invoke(true);
        EnableThrow(false);
        hookMovement.SetPosition();
        hookMovement.StealDirection();

    }
    void EnableThrow(bool canHook)
    {
        _canThrow = canHook;
    }
    void EnableHook(){
        HookPrefab.SetActive(true);
    }
    public void DisableHook(){
        HookPrefab.SetActive(false);
    }
}