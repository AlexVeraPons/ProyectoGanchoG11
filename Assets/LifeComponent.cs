using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LifeComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private InputAction _debugTakeDamageAction;
    [SerializeField] int _current = 1;
    public int Current => _current;
    [SerializeField] int _max = 1;

    private void Start()
    {
        _current = _max;    
    }

    public static Action OnDeath;
    public static Action OnHit;

    private void Awake()
    {
        _debugTakeDamageAction.Enable();
    }

    private void Update()
    {
        //Debug stuff
        /*
        if(_debugTakeDamageAction.triggered)
        {
            print("Debug Action: TakeDamage(1) executed.");
            TakeDamage(1);
        } 
        */   
    }

    public void TakeDamage(int ammount)
    {
        if(IsDead() == false)
        {
            _current = Math.Max(_current - ammount, 0);
            if(_current > 0)
            {
                OnHit?.Invoke();
            }
            else
            {
                OnDeath?.Invoke();
            }
        }
    }

    bool IsDead()
    {
        return _current == 0;
    }
}
