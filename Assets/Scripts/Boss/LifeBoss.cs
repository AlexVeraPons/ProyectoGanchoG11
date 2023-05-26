using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoss : MonoBehaviour
{
    public static Action OnBossDeath;

    [SerializeField]
    private List<GameObject> lifeBoss = new List<GameObject>();

    private int _maxLifes;
    private int _currentLifes;
    
    private void OnEnable()
    {
        LifeComponent.OnDeath += ResetLifes;
        Collectible.OnCollected += RemoveOneLife;
    }

    private void Start()
    {
        _maxLifes = lifeBoss.Count;
        _currentLifes = _maxLifes;
        ResetLifes();
    }

    public void RemoveOneLife()
    {
        print("dw");
        if (lifeBoss.Count > 0 && _currentLifes > 0)
        {
            GameObject life = lifeBoss[_currentLifes - 1];
            life.SetActive(false);
            _currentLifes--;
        }

        if (_currentLifes == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    public void ResetLifes()
    {
        foreach (GameObject life in lifeBoss)
        {
            life.SetActive(true);
        }
    }

    public void SetLifes(int lifes)
    {
        if (lifes > lifeBoss.Count)
        {
            lifes = lifeBoss.Count;
        }
    }
}
