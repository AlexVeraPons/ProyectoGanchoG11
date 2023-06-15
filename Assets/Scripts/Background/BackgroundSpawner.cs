using UnityEngine;
using System;

public class BackgroundSpawner : MonoBehaviour
{
    public Action OnShoot;
    public Action OnEnterBossStage;

    [Space(5)]

    [Header("TIME related")]
    [Space(5)]

    [SerializeField]
    [Tooltip("time it takes to spawn the next wave")]
    private float _spawnTime;
    private float _time;

    void Start()
    {
        _time = _spawnTime;
    }

    void Update()
    {
        Timer();
    }
    void Timer()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            Shoot();
            ResetTimer();
        }
    }
    void Shoot()
    {
        OnShoot?.Invoke();
    }
    void ResetTimer()
    {
        _time = _spawnTime;
    }

    public void EnterBossStage()
    {
        DisableSpriteRenderer();
        ChangeCannonsToBossState();
    }

    private void DisableSpriteRenderer()
    {
        this.gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void ChangeCannonsToBossState()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Cannons>() != null)
            {
                child.gameObject.GetComponent<Cannons>().ChangeToBossBullet();
            }
        }
    }
}