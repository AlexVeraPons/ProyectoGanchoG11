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

        if (Input.GetKey(KeyCode.Space))
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
        if (hitInfo.tag == "Hook")
        {   
            HookPrefab.SetActive(false);
            EnableThrow(true);
        }
    }
    void Throw()
    {
        HookPrefab.SetActive(true);
        hookIsMovingRight?.Invoke(true);
        EnableThrow(false);
        hookMovement.SetPosition();
        //_canThrow = false;
        //var hook = Instantiate(HookPrefab, transform.position, Quaternion.identity);
        //hookMovement = hookMovement.GetComponent<HookMovement>();
        //hookMovement.ChangeHookDirection(true);
        //hook.transform.SetParent(this.transform);
        hookMovement.StealDirection();

    }
    void EnableThrow(bool canHook)
    {
        _canThrow = canHook;
    }
}