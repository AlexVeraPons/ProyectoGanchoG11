using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private GameObject _bullet;

    private void OnEnable()
    {
        _backgroundSpawner.OnShoot += Shoot;
    }
    private void OnDisable()
    {
        _backgroundSpawner.OnShoot -= Shoot;

    }
    void Start()
    {
        _backgroundSpawner = GetComponentInParent<BackgroundSpawner>();
    }

    private void Shoot()
    {
        foreach (Transform objects in _spawnPositions)
        {
            Instantiate(_bullet, objects.transform.position, Quaternion.identity, objects.transform);
            Debug.Log("OnShootFet");
        }
    }
    public Vector3 GetDirection()
    {
        return _direction;
    }
    public bool GetInverted(){
        return _shouldInvert;
    }
}
