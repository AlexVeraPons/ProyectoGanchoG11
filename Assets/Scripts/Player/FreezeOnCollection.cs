using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnCollection : MonoBehaviour
{
    private Rigidbody2D _rigibody2D;

    void Awake()
    {
        _rigibody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        LifeComponent.OnDeath += TurnStatic;
        WaveManager.OnUnloadWave += TurnStatic;
        WaveManager.OnLoadWave += TurnDynamic;
    }

    private void OnDisable()
    {
        LifeComponent.OnDeath -= TurnStatic;
        WaveManager.OnUnloadWave -= TurnStatic;
        WaveManager.OnLoadWave -= TurnDynamic;
    }
    
    void TurnStatic()
    {
        _rigibody2D.bodyType = RigidbodyType2D.Static;
    }

    void TurnDynamic()
    {
        _rigibody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}