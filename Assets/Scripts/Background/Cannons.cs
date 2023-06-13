using UnityEngine;

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
    public bool GetInverted(){
        return _shouldInvert;
    }
    public void ChangeToNormalBullet(){
        _bulletToSpawn = _bullet;
        _objectPool.Initialize(_bulletToSpawn, _bulletPoolCount);
    }
    public void ChangeToBossBullet(){
        _bulletToSpawn = _bossBullet;
        _objectPool.Initialize(_bulletToSpawn, _bulletPoolCount);
    }
}
