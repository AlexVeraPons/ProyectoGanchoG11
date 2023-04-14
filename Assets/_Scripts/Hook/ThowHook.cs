using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThowHook : MonoBehaviour
{
    public GameObject BulletPrefab;
    public static Action<bool> hookIsMovingRight;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hook");
            hookIsMovingRight?.Invoke(true);
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hookIsMovingRight?.Invoke(false);

        }
    }
}
