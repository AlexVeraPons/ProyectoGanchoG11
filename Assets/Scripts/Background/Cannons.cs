using UnityEngine;
using System;

[RequireComponent(typeof(ObjectPool))]
public class Cannons : MonoBehaviour
{
    [SerializeField]
    private BackgroundSpawner _backgroundSpawner;

    [SerializeField]
    [Tooltip("Direction for the bullets")]
    private Vector3 _direction;

    [Space(5)]
    [SerializeField]
    [Tooltip("if the bulets tend to go to the middle this should be checked")]
    private bool _shouldInvert;

    [SerializeField]
    private Transform[] _spawnPositions;

    private ObjectPool _objectPool;
    [SerializeField]
    private int _bulletPoolCount = 10;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _bossBullet;
    private GameObject _bulletToSpawn;

    public Action OnEnterBoss;

    [Header("Color of The Boss Bullets")]
    [SerializeField]
    private Color _colorOfNewBullets;

    [Space(5)]
    [Header("Color of The Start Bullet Trail")]
    [SerializeField]
    private Color _colorOfStartBulletTrail;
    [Space(5)]
    [Header("Color of start Bullet trail")]
    [SerializeField]
    private Color _colorOfEndBulletTrail;

    private void OnEnable()
    {
        _backgroundSpawner.OnShoot += Shoot;
        _backgroundSpawner.OnEnterBossStage += ChangeToBossBullet;
    }
    private void OnDisable()
    {
        _backgroundSpawner.OnShoot -= Shoot;
        _backgroundSpawner.OnEnterBossStage -= ChangeToBossBullet;

    }
    void Start()
    {

        _backgroundSpawner = GetComponentInParent<BackgroundSpawner>();
        _objectPool = GetComponent<ObjectPool>();

        _bulletToSpawn = _bullet;
        _objectPool.Initialize(_bulletToSpawn, _bulletPoolCount);
    }

    private void Shoot()
    {
        foreach (Transform objects in _spawnPositions)
        {
            GameObject bullet = _objectPool.CreateObject(objects);
        }
    }
    public Vector3 GetDirection()
    {
        return _direction;
    }
    public bool GetInverted()
    {
        return _shouldInvert;
    }

    public void ChangeToBossBullet()
    {
        _bulletToSpawn = _bossBullet;
        _objectPool.Initialize(_bulletToSpawn, _bulletPoolCount);
        OnEnterBoss?.Invoke();
    }
    public Color GetBulletColor()
    {
        return _colorOfNewBullets;
    }
    public Color GetStartBulletTrailColor()
    {
        return _colorOfStartBulletTrail;
    }
    public Color GetEndBulletTrailColor()
    {
        return _colorOfEndBulletTrail;
    }
}
