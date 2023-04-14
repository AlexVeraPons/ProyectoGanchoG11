using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThowHook : MonoBehaviour
{
    public GameObject BulletPrefab;
    public static Action<bool> hookIsMovingRight;

    public Vector2 InputtedDirection { get; private set; }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(true);
            Throw();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(false);

        }
    }
    void Throw()
    {
        var hook = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var hookMovement = hook.GetComponent<HookMovement>();
        hookMovement.SetDirection(InputtedDirection);
        hook.transform.SetParent(this.transform);
    }
}
