using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThowHook : MonoBehaviour
{
    public GameObject HookPrefab;
    public static Action<bool> hookIsMovingRight;
    private bool _canThrow;

    public Vector2 InputtedDirection { get; private set; }

    void OnEnable()
    {
        HookMovement.OnHookEntersPlayer += enableThrow;
    }
    void OnDisable()
    {
        HookMovement.OnHookEntersPlayer -= enableThrow;

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
                hookIsMovingRight?.Invoke(true);
                Throw();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(false);

        }
    }
    void Throw()
    {

        _canThrow = false;
        var hook = Instantiate(HookPrefab, transform.position, Quaternion.identity);
        var hookMovement = hook.GetComponent<HookMovement>();
        //hook.transform.SetParent(this.transform);
        hookMovement.StealDirection();

    }
    void enableThrow(bool canHook)
    {
        _canThrow = canHook;
        hookIsMovingRight?.Invoke(true);

    }
}
