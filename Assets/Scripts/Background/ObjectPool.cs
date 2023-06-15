using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private bool _isInBoss;
    private Cannons _cannons;
    private CPUBullet _cpuBullet;

    [SerializeField]
    protected GameObject objectToPool;

    [SerializeField]
    protected int poolSize = 10;

    protected Queue<GameObject> objectPool;

    private void Awake()
    {
        _cannons = GetComponent<Cannons>();

        objectPool = new Queue<GameObject>();
    }
    private void OnEnable()
    {
        _cannons.OnEnterBoss += SetEnterBoss;
    }
    private void OnDisable()
    {
        _cannons.OnEnterBoss += SetEnterBoss;
    }

    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject(Transform parent)
    {
        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, parent.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(parent);
            if (spawnedObject.GetComponent<CPUBullet>() != null)
            {
                var cpuBullet = spawnedObject.GetComponent<CPUBullet>();
                cpuBullet.ResetTimer();
            }

        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = parent.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
            if (spawnedObject.GetComponent<CPUBullet>() != null)
            {
                var cpuBullet = spawnedObject.GetComponent<CPUBullet>();
                cpuBullet.ResetTimer();
                cpuBullet.ActivateTrail();
                if (_isInBoss)
                {
                    cpuBullet.ChangeBulletToBossMode();
                }
            }
        }

        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }
    private void SetEnterBoss()
    {
        _isInBoss = true;
    }
}
