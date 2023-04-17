using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThowHook : MonoBehaviour
{
    public GameObject HookPrefab;
    public HookMovement hookMovement;
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
                Throw();
                Debug.Log("pulsa");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(false);
            Debug.Log("suelta");
        }
    }
    void Throw()
    {

        hookIsMovingRight?.Invoke(true);
        enableThrow(false);
        //_canThrow = false;
        //var hook = Instantiate(HookPrefab, transform.position, Quaternion.identity);
        //hookMovement = hookMovement.GetComponent<HookMovement>();
        //hookMovement.ChangeHookDirection(true);
        //hook.transform.SetParent(this.transform);
        hookMovement.StealDirection();

    }
    void enableThrow(bool canHook)
    {
        _canThrow = canHook;

    }
}
